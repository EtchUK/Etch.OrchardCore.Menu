using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentFields.Settings;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;

namespace Etch.OrchardCore.Menu
{
    public class EmailMenuItemMigrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private const string EmailMenuItem = "EmailMenuItem";

        public EmailMenuItemMigrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(EmailMenuItem, builder => builder
                .WithField("EmailAddress", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Email Address")
                    .WithEditor("Email")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Email address that will be pre-populated the to field in the new email message.",
                        Required = true
                    })
                    .WithPosition("1")));

            await _contentDefinitionManager.AlterTypeDefinitionAsync(EmailMenuItem, builder => builder
                .Stereotype("MenuItem")
                .WithPart(EmailMenuItem, builder => builder
                    .WithPosition("1"))
                .WithPart("LinkVisualPart", builder => builder
                    .WithPosition("2")));

            return 1;
        }

        public async Task<int> UpdateFrom1Async()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(EmailMenuItem, builder => builder
                .WithField("Subject", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Subject")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Optionally pre-populate the subject field.",
                        Required = false
                    })
                    .WithPosition("2")));

            return 2;
        }

        public async Task<int> UpdateFrom2Async()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(EmailMenuItem, builder => builder
                .WithField("Label", field => field
                    .OfType(nameof(TextField))
                    .WithDisplayName("Label")
                    .WithSettings(new TextFieldSettings
                    {
                        Hint = "Label displayed on link to users.",
                        Required = true
                    })
                    .WithPosition("0")));

            return 3;
        }
    }
}
