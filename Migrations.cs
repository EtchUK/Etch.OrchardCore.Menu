using Etch.OrchardCore.Menu.Helpers;
using Etch.OrchardCore.Menu.Models;
using Etch.OrchardCore.Widgets.Models;
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
        private readonly ICommonMenuItemPartMigrator _commonMenuItemPartMigrator;
        private readonly IRecipeMigrator _recipeMigrator;

        public Migrations(ICommonMenuItemPartMigrator commonMenuitemPartMigrator, IContentDefinitionManager contentDefinitionManager, IRecipeMigrator recipeMigrator)
        {
            _contentDefinitionManager = contentDefinitionManager;
            _commonMenuItemPartMigrator = commonMenuitemPartMigrator;
            _recipeMigrator = recipeMigrator;
            _commonMenuItemPartMigrator = commonMenuitemPartMigrator;
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

        public async Task<int> UpdateFrom3Async()
        {
            await _contentDefinitionManager.AlterTypeDefinitionAsync("ContentMenuItem", builder => builder
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("LinkMenuItem", builder => builder
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            return 4;
        }

        public async Task<int> UpdateFrom4Async()
        {
            await _recipeMigrator.ExecuteAsync("update4.recipe.json", this);
            return 5;
        }

        public async Task<int> UpdateFrom5Async()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync("LabelMenuItem", builder => builder
                .RemoveField("Style"));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("LabelMenuItem", builder => builder
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            return 6;
        }

        public async Task<int> UpdateFrom6Async()
        {
            await _contentDefinitionManager.AlterTypeDefinitionAsync ("ContentMenuItem", builder => builder
                .RemovePart(nameof(CommonMenuItem))
                .WithPart(nameof(LinkBehaviourPart), part => part
                    .WithPosition("15")));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("ImageMenuItem", builder => builder
                .RemovePart(nameof(CommonMenuItem))
                .WithPart(nameof(LinkDestinationPart), part => part
                    .WithPosition("10"))
                .WithPart(nameof(LinkBehaviourPart), part => part
                    .WithPosition("15")));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("LinkMenuItem", builder => builder
                .RemovePart(nameof(CommonMenuItem))
                .WithPart(nameof(LinkBehaviourPart), part => part
                    .WithPosition("15")));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("NotificationMenuItem", builder => builder
                .RemovePart(nameof(CommonMenuItem))
                .WithPart(nameof(LinkBehaviourPart), part => part
                    .WithPosition("15")));

            await _contentDefinitionManager.AlterTypeDefinitionAsync("SVGMenuItem", builder => builder
                .RemovePart(nameof(CommonMenuItem))
                .WithPart(nameof(LinkDestinationPart), part => part
                    .WithPosition("10"))
                .WithPart(nameof(LinkBehaviourPart), part => part
                    .WithPosition("15")));

            return 7;
        }

        public async Task<int> UpdateFrom7Async()
        {
            await _commonMenuItemPartMigrator.MigrateAsync();
            return 8;
        }
    }
}
