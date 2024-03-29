using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Navigation",
    Description = "Provides additional menu items content types.",
    Name = "Custom Menu Items",
    Version = "$(VersionNumber)",
    Website = "https://etchuk.com",
    Dependencies = new[] { "Etch.OrchardCore.Fields.Code", "Etch.OrchardCore.Fields.Query", "OrchardCore.Media", "OrchardCore.Menu", "Etch.OrchardCore.Widgets" }
)]