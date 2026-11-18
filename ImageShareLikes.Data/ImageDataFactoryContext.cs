using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageShareLikes.Data
{
    public class ImageDataFactoryContext : IDesignTimeDbContextFactory<ImageDataContext>
    {
        public ImageDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                $"..{Path.DirectorySeparatorChar}ImageShareLikes.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageDataContext(config.GetConnectionString("ConStr"));
        }
    }
}
