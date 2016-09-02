using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using com.pmp.mongo.data;
using com.pmp.mongo.client;

namespace com.pmp.mongo.service
{
    public class BaseService<T> where T : MgBaseModel, new()
    {

        /// <summary>
        /// 创建实例T 的自增长起始值：0 
        /// 每个T(Model)只需调用一次
        /// </summary>
        public void CreateCounter()
        {
            MgClient.CreateDefaultCounter<T>();
        }


        /// <summary>
        /// 获取一个新的自增Id
        /// </summary>
        /// <returns></returns>
        public int GetNewId()
        {
           return MgClient.CreateNewId<T>();
        }

        public void Insert(T model)
        {
            MgClient.Insert<T>(model);
        }

        public long Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return MgClient.Update<T>(filter, update);
        }

        public List<T> Search(FilterDefinition<T> filter)
        {
            return MgClient.Search<T>(filter);
        }

        public List<T> Search()
        {
            return MgClient.Search<T>();
        }

        public long Delete(FilterDefinition<T> filter)
        {
            return MgClient.Delete<T>(filter);
        }

        public string Index(IndexKeysDefinition<T> indexKeys)
        {
            return MgClient.Index<T>(indexKeys);
        }

        public List<BsonDocument> Aggregate(FilterDefinition<T> filter, ProjectionDefinition<T> group)
        {
            return MgClient.Aggregate(filter, group);
        }




        

    }
}
