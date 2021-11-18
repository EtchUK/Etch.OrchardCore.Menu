using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Navigation",
    Description = "Provides additional menu items content types.",
    Name = "Custom Menu Items",
    Version = "1.1.0",
    Website = "https://etchuk.com",
    Dependencies = new[] { "Etch.OrchardCore.Fields.Code", "OrchardCore.Media", "OrchardCore.Menu", "Etch.OrchardCore.Widgets" }
)]