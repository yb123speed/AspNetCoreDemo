using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBSample.Data
{
    /// <summary>
    /// MongoDB对象的上下文
    /// </summary>
    public class MongoDbContext
    {
        /// <summary>
        /// Mongo上下文
        /// </summary>
        public IMongoDatabase DbContext { get; set; }
        public ILogger _logger { get; set; }

        public MongoDbContext(string dbName)
        {
            _logger = new LoggerFactory().CreateLogger(typeof(MongoDbContext));
            //连接字符串，如："mongodb://username:password@host:port/[DatabaseName]?ssl=true"
            //建议放在配置文件中
            var connectionString = "mongodb://yebin:yb123speed@localhost:27017";
            try
            {
                var mongoClient = new MongoClient(connectionString);
                //数据库如果不存在，会自动创建
                DbContext = mongoClient.GetDatabase(dbName);
            }
            catch(Exception e)
            {
                _logger.LogError("构建MongoDbContext出错", e);
                throw;
            }
        }

        /// <summary>
        /// 异步获取表（集合）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public async Task<IMongoCollection<TEntity>> GetCollectionAsync<TEntity>(string tableName = "") where TEntity : class
        {


            var dt = DateTime.Now.ToString("yyyy -MM-dd");

            if (!string.IsNullOrEmpty(tableName))
            {

                dt = tableName;
            }

            // 获取集合名称，使用的标准是在实体类型名后添加日期
            var collectionName = dt;

            // 如果集合不存在，那么创建集合
            if (false == await IsCollectionExistsAsync<TEntity>(collectionName))
            {
                await DbContext.CreateCollectionAsync(collectionName);
            }


            return DbContext.GetCollection<TEntity>(collectionName);
        }


        /// <summary>
        /// 集合是否存在
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<bool> IsCollectionExistsAsync<TEntity>(string name)
        {
            var filter = new BsonDocument("name", name);
            // 通过集合名称过滤
            var collections = await DbContext.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
            // 检查是否存在
            return await collections.AnyAsync();
        }
    }
}
