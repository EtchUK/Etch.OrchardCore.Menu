using Etch.OrchardCore.Fields.Code.Fields;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.Menu.Models
{
    public class CommonMenuItem : ContentPart
    {
        public TextField ExternalUrl { get; set; }
        public ContentPickerField LinkTo { get; set; }
        public CodeField OnClick { get; set; }
        public BooleanField OpenNewTab { get; set; }
    }
}