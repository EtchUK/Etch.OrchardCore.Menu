using OrchardCore.Modules.Manifest;

[assembly: Module(
    Author = "Etch UK Ltd.",
    Category = "Navigation",
    Description = "Provides custom menu items that aren't available by default.",
    Name = "Custom Menu Items",
    Version = "0.2.0",
    Website = "https://etchuk.com",
    Dependencies = new[] { "Etch.OrchardCore.Fields.Code", "OrchardCore.Media" }
)]