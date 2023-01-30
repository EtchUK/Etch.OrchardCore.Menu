using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCore.Recipes.Services;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Menu
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IRecipeMigrator _recipeMigrator;

        public Migrations(IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _recipeMigrator = recipeMigrator;
        }

        public async Task<int> CreateAsync()
        {
            await _recipeMigrator.ExecuteAsync("create.recipe.json", this);
            return 1;
        }

        public async Task<int> UpdateFrom1Async()
        {
            await _recipeMigrator.ExecuteAsync("update1.recipe.json", this);
            return 2;
        }

        public async Task<int> UpdateFrom2Async()
        {
            await _recipeMigrator.ExecuteAsync("update2.recipe.json", this);
            return 3;
        }

        public int UpdateFrom3()
        {
            _contentDefinitionManager.AlterTypeDefinition("ContentMenuItem", builder => builder
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            _contentDefinitionManager.AlterTypeDefinition("LinkMenuItem", builder => builder
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            return 4;
        }

        public async Task<int> UpdateFrom4Async()
        {
            await _recipeMigrator.ExecuteAsync("update4.recipe.json", this);
            return 5;
        }

        public int UpdateFrom5()
        {
            _contentDefinitionManager.AlterPartDefinition("LabelMenuItem", builder => builder
                .RemoveField("Style"));

            _contentDefinitionManager.AlterTypeDefinition("LabelMenuItem", builder => builder
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            return 6;
        }

        public int UpdateFrom6()
        {
            _contentDefinitionManager.AlterTypeDefinition("ContentMenuItem", builder => builder
                .WithPart("LinkBehaviourPart")); 

            _contentDefinitionManager.AlterTypeDefinition("ImageMenuItem", builder => builder
                .RemovePart("CommonMenuItem")
                .WithPart("LinkDestinationPart")
                .WithPart("LinkBehaviourPart")); 

            _contentDefinitionManager.AlterTypeDefinition("LinkMenuItem", builder => builder
                .WithPart("LinkBehaviourPart")); 

            _contentDefinitionManager.AlterTypeDefinition("NotificationMenuItem", builder => builder
                .WithPart("LinkBehaviourPart"));

            _contentDefinitionManager.AlterTypeDefinition("SVGMenuItem", builder => builder
                .RemovePart("CommonMenuItem")
                .WithPart("LinkDestinationPart")
                .WithPart("LinkBehaviourPart"));

            return 7;
        }
    }
}
