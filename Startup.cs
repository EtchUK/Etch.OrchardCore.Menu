using Etch.OrchardCore.Menu.Helpers;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.Menu
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IDataMigration, EmailMenuItemMigrations>();

            services.AddScoped<ICommonMenuItemPartMigrator, CommonMenuItemPartMigrator>();
        }
    }
}
