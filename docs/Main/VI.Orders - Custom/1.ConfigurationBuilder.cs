using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace eShopSolution.Data.EF
{
    public class EShopDbContextFactory : IDesignTimeDbContextFactory<EShopDbContext>
    {
        public EShopDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("eShopSolutionDb"); 
        }
    }
}

/*
    # Cài các Package
    dotnet add package Microsoft.Extensions.Configuration
    dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions
    dotnet add package Microsoft.Extensions.Configuration.Json

    # Tạo ra đối tượng ConfigBuilder để nạp cấu hình từ  file [appsettings.json]
    var configBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())      // file config ở thư mục hiện tại
                        .AddJsonFile("appsettings.json");                  // nạp config định dạng JSON
    var configurationroot = configBuilder.Build();                            // Tạo configurationroot

    # Sử dụng 
    var value = configurationroot.GetSection("key")

    # Kết hợp [services] đăng  ký [IOptionService]
    services.Configure<MyServiceOptions>(configurationroot.GetSection("MyServiceOptions"));
*/