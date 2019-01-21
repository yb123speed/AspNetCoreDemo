using System.Threading.Tasks;
using SimpleTaskApp.Web.Controllers;
using Shouldly;
using Xunit;
using SimpleTaskApp.Tasks;
using Task = System.Threading.Tasks.Task;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AngleSharp.Html.Parser;

namespace SimpleTaskApp.Web.Tests.Controllers
{
    public class TasksController_Tests: SimpleTaskAppWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<TasksController>(nameof(TasksController.Index), new
                {
                    state = TaskState.Open
                })
            );
            
            //Assert
            response.ShouldNotBeNullOrWhiteSpace();

            var tasksInDatabase = await UsingDbContextAsync(async context =>
            {
                return await context.Tasks.Where(t => t.State == TaskState.Open).ToListAsync();
            });

            var document = new HtmlParser().ParseDocument(response);
            var listItems = document.QuerySelectorAll("#TaskList li");

            listItems.Length.ShouldBe(tasksInDatabase.Count);

            foreach (var listItem in listItems)
            {
                var header = listItem.QuerySelector(".list-group-item-heading");
                var taskTitle = header.InnerHtml.Trim();
                tasksInDatabase.Any(t => t.Title == taskTitle).ShouldBeTrue();
            }
        }
    }
}
