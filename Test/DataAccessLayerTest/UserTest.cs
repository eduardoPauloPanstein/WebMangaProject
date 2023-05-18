using DataAccessLayer;
using DataAccessLayer.Implementations;
using Entities.UserS;
using Microsoft.Extensions.DependencyInjection;

namespace Test.DataAccessLayerTest
{
    public class UserTest : IClassFixture<DbFixture>
    {
        private ServiceProvider _serviceProvider;

        public UserTest(DbFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void GetUser_ReturnNotNull()
        {
            using (var context = _serviceProvider.GetService<MangaProjectDbContext>())
            {
                UserDAL u = new(context);

                // Act  
                var response = u.Get(5);
                var user = response.Result.Item;

                //Assert  
                Assert.NotNull(user);
            }

        }

        [Fact]
        public void GetUserMangaList_ReturnNotNull()
        {
            using (var context = _serviceProvider.GetService<MangaProjectDbContext>())
            {
                UserDAL u = new(context);

                // Act  
                var response = u.Get(5);
                var user = response.Result.Item;
                var mangaList = user.MangaList;

                //Assert  
                Assert.NotNull(mangaList);
            }

        }
        [Fact]
        public void UpdateUser_ReturnSuccess()
        {
            using (var context = _serviceProvider.GetService<MangaProjectDbContext>())
            {
                UserDAL u = new(context);

                // Act  
                //var response = u.Get(8);

                User user = new()
                {
                    Nickname = "edup",
                    AvatarImageFileLocation = "edupAvatar.jpg",
                    About = "oie"
                };

                var response = u.Update(user);


                //Assert  
                Assert.True(response.Result.HasSuccess);
            }

        }
    }
}
