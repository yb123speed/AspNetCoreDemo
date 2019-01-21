using System.Threading.Tasks;
using SimpleTaskApp.Web.Controllers;
using Shouldly;
using Xunit;

namespace SimpleTaskApp.Web.Tests.Controllers
{
    public class HomeController_Tests: SimpleTaskAppWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}
