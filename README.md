# Etch.OrchardCore.Menu

Module for [Orchard Core](https://github.com/orchardcms/OrchardCore) that provides additional menu items content types.

## Build Status

[![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.Menu.svg)](https://www.nuget.org/packages/Etch.OrchardCore.Menu)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.6.0`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.6.0)).

## Installing

This module is available on [NuGet](https://www.nuget.org/packages/Etch.OrchardCore.Menu). Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.Menu", ensuring include prereleases is checked.

Alternatively you can [download the source](https://github.com/etchuk/Etch.OrchardCore.Menu/archive/master.zip) or clone the repository to your local machine. Add the project to your solution that contains an Orchard Core project and add a reference to Etch.OrchardCore.Menu.

## Usage

Enabled the "Custom Menu Items" feature, which will make add the menu item content types displayed below.

### Image Menu Item

Content editors can choose an image from the media library that will be displayed on the front-end linked to either a content item or an external URL.

### Notifcation Menu Item

Display a count driven by a query alongside a menu item, an example would be a count of the number of available jobs.

### SVG Menu Item

This menu item contains a code field that should be populated with an `svg` element that will be displayed on the front-end linked to either a content item or an external URL.

## Packaging

When the theme is compiled (using `dotnet build`) it's configured to generate a `.nupkg` file (this can be found in `\bin\Debug\` or `\bin\Release`).

## Notes

This theme was created using `v0.4.2` of [Etch.OrchardCore.ModuleBoilerplate](https://github.com/EtchUK/Etch.OrchardCore.Menu) template.
