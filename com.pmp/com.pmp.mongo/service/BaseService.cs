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
