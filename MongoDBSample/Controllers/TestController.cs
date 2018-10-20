using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBSample.Data;
using MongoDBSample.Models;

namespace MongoDBSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMongoRepository<ActionLog> _IMongoRepository;

        public TestController(IMongoRepository<ActionLog> __IMongoRepository)
        {
            _IMongoRepository = __IMongoRepository;
        }

        /// <summary>
        /// 测试新增数据方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Add()
        {
            //创建两个不同企业ID的实体数据
            var model1 = new ActionLog();
            model1.CompanyId = Guid.Parse("B29BC831-A974-4114-90E2-0001E03FBCAF");
            model1.ActionLogId = Guid.NewGuid();
            model1.Context = "test1";
            model1.CreateTime = DateTime.Now;
            model1.UpdateTime = DateTime.Now;

            var model2 = new ActionLog
            {
                CompanyId = Guid.Parse("651bbe49-a4c8-4514-babb-897dad7065e3"),
                ActionLogId = Guid.NewGuid(),
                Context = "test2",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };


            var list = new List<ActionLog>();
            list.Add(model1);
            list.Add(model2);

            var group_list = list.GroupBy(p => p.CompanyId);
            var tableName = "ActionLog_" + DateTime.Now.ToString("yyyy-MM-dd");
            foreach (var group in group_list)
            {
                var dbName = "ActionLog_" + group.FirstOrDefault().CompanyId.ToString();

                await _IMongoRepository.Add(group.ToList(), dbName, tableName);
            }

            return "value1";
        }

        /// <summary>
        /// 测试查询方法
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("{companyId}")]
        public async Task<List<ActionLog>> Get(Guid companyId)
        {
            var dbName = "ActionLog_" + companyId.ToString();
            var tableName = "ActionLog_" + DateTime.Now.ToString("yyyy-MM-dd");
            var list = await _IMongoRepository.GetListAsync(p => p.Context.IndexOf("t") > -1, dbName, tableName);
            return list;
        }
    }
}