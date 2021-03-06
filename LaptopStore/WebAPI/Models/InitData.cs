using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class InitData
    {
        const string defaultAdminUserName = "DefaultAdminUserName";
        const string defaultAdminPassword = "DefaultAdminPassword";

        public static async Task InitializeLaptopStoreDatabaseAsync (IServiceProvider serviceProvider, bool createUser = true)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var db = scopeServiceProvider.GetService<LaptopStoreContext>();
                var result = await db.Database.EnsureCreatedAsync();
                if (result)
                {
                    //await InsertSampleData(scopeServiceProvider);

                    if (createUser == true)
                    {
                        await CreateAdminUser(scopeServiceProvider);
                    }
                }
                
            }
        }

        //private static async Task InsertSampleData(IServiceProvider serviceProvider)
        //{

        //    var albums = GetAlbums(imgUrl, Genres, Artists);

        //    await AddOrUpdateAsync(serviceProvider, g => g.GenreId, Genres.Select(genre => genre.Value));
        //    await AddOrUpdateAsync(serviceProvider, a => a.ArtistId, Artists.Select(artist => artist.Value));
        //    await AddOrUpdateAsync(serviceProvider, a => a.AlbumId, albums);
        //}

        private static async Task AddOrUpdateAsync<TEntity>(
            IServiceProvider serviceProvider,
            Func<TEntity, object> propertyToMatch, IEnumerable<TEntity> entities)
            where TEntity : class
        {
            // Query in a separate context so that we can attach existing entities as modified
            List<TEntity> existingData;
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<LaptopStoreContext>();
                existingData = db.Set<TEntity>().ToList();
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<LaptopStoreContext>();
                foreach (var item in entities)
                {
                    db.Entry(item).State = existingData.Any(g => propertyToMatch(g).Equals(propertyToMatch(item)))
                        ? EntityState.Modified
                        : EntityState.Added;
                }

                await db.SaveChangesAsync();
            }
        }

        private static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var env = serviceProvider.GetService<IHostingEnvironment>();

            var builder = new ConfigurationBuilder()
                              .SetBasePath(env.ContentRootPath)
                              .AddJsonFile("appsettings.json")
                              .AddEnvironmentVariables();
            var configuration = builder.Build();

            var userManager = serviceProvider.GetService<UserManager<UserEntity>>();

            var user = await userManager.FindByNameAsync(configuration[defaultAdminUserName]);

            if (user == null)
            {
                var createUser = new UserEntity() {
                    UserName = configuration[defaultAdminUserName],
                    Email = configuration[defaultAdminUserName]  
                };
                await userManager.CreateAsync(createUser, configuration[defaultAdminPassword]);
                await userManager.AddClaimAsync(createUser, new Claim("Admin", "Allowed"));
            }
        }
    }
}