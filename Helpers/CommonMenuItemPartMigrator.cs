using Etch.OrchardCore.Menu.Models;
using Etch.OrchardCore.Widgets.Models;
using GraphQL;
using Microsoft.Extensions.Logging;
using OrchardCore.Autoroute.Models;
using OrchardCore.ContentFields.Fields;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Records;
using OrchardCore.Environment.Shell;
using OrchardCore.Menu.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YesSql;

namespace Etch.OrchardCore.Menu.Helpers
{
    public class CommonMenuItemPartMigrator : ICommonMenuItemPartMigrator
    {
        private readonly IContentManager _contentManager;
        private readonly ILogger<CommonMenuItemPartMigrator> _logger;
        private readonly ISession _session;
        private readonly ShellSettings _settings;

        public CommonMenuItemPartMigrator(IContentManager contentManager,
            ILogger<CommonMenuItemPartMigrator> logger,
            ISession session,
            ShellSettings settings)
        {
            _contentManager = contentManager;
            _logger = logger;
            _session = session;
            _settings = settings;
        }

        public async Task MigrateAsync()
        {
            foreach (var contentItem in await FetchMenuItemsAsync())
            {
                var menuItemsListPart = await MigrateMenuItemsAsync(contentItem.As<MenuItemsListPart>());

                contentItem.Apply(nameof(MenuItemsListPart), menuItemsListPart);
                await _contentManager.UpdateAsync(contentItem);
                await _contentManager.PublishAsync(contentItem);
            }
        }

        private async Task<string> ContentItemIdToUrlAsync(string contentItemId)
        {
            var path = "/";

            if (!string.IsNullOrEmpty(_settings.RequestUrlPrefix))
            {
                path += _settings.RequestUrlPrefix + "/";
            }

            var contentItem = await _session.Query<ContentItem>()
                .With<ContentItemIndex>()
                    .Where(x => x.Latest && x.ContentItemId == contentItemId)
                .FirstOrDefaultAsync();

            if (contentItem == null)
            {
                return path;
            }

            if (contentItem.Has<AutoroutePart>())
            {
                return $"{path}{contentItem.As<AutoroutePart>().Path}";
            }

            return $"{path}Contents/ContentItems/{contentItemId}";
        }

        private async Task<IEnumerable<ContentItem>> FetchMenuItemsAsync()
        {
            return await _session.Query<ContentItem>()
                .With<ContentItemIndex>()
                    .Where(x => x.Latest && x.ContentType == "Menu")
                .ListAsync();
        }

        private async Task<MenuItemsListPart> MigrateMenuItemsAsync(MenuItemsListPart menuItemsListPart)
        {
            foreach (var linkMenuItem in menuItemsListPart.MenuItems)
            {
                var linkDestinationPart = new LinkDestinationPart();

                try
                {
                    if (!linkMenuItem.Has<CommonMenuItem>())
                    {
                        continue;
                    }

                    if (linkMenuItem.As<CommonMenuItem>().ExternalUrl.Text != null)
                    {
                        linkDestinationPart.DestinationUrl = linkMenuItem.As<CommonMenuItem>().ExternalUrl;
                    }
                    else
                    {
                        linkDestinationPart.DestinationUrl = new TextField { Text = await ContentItemIdToUrlAsync(linkMenuItem.As<CommonMenuItem>().LinkTo.ContentItemIds[0]) };
                    }

                    var linkBehaviourPart = new LinkBehaviourPart
                    {
                        ClickEvent = linkMenuItem.As<CommonMenuItem>().OnClick,
                        OpenIn = new TextField { Text = linkMenuItem.As<CommonMenuItem>().OpenNewTab.Value ? "_blank" : "_self" }
                    };

                    linkMenuItem.Apply(nameof(LinkDestinationPart), linkDestinationPart);
                    linkMenuItem.Apply(nameof(LinkBehaviourPart), linkBehaviourPart);
                    ContentExtensions.Apply(linkMenuItem, linkMenuItem);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update menu item: {0} ID: {1}", linkMenuItem.ContentType, linkMenuItem.ContentItemId);
                }
            }

            return menuItemsListPart;
        }
    }

    public interface ICommonMenuItemPartMigrator
    {
        Task MigrateAsync();
    }
}