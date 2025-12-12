using GestaoBiblioteca.Core.Context.Settings;
using GestaoBiblioteca.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace GestaoBiblioteca.Api.Configurations
{
    public static class SqlServerConfiguration
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var sqlSettings = configuration.GetSection("DatabaseSettings:SqlServerSettings").Get<SqlServerSettings>();

            // Adicionar DbContext ao provedor de serviços
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(sqlSettings.ConnectionString));
        }
    }
}
