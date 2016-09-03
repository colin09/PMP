﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using com.pmp.mongo.data;
using com.pmp.mongo.client;
using System.Linq.Expressions;

namespace com.pmp.mongo.service
{
    public class BaseService<T> where T : MgBaseModel, new()
    {

        /// <summary>
        /// 创建实例T 的自增长起始值：0 
        /// 每个T(Model)只需调用一次
        /// </summary>
        protected void CreateCounter()
        {
            MgClient.CreateDefaultCounter<T>();
        }


        /// <summary>
        /// 获取一个新的自增Id
        /// </summary>
        /// <returns></returns>
        protected int GetNewId()
        {
           return MgClient.CreateNewId<T>();
        }

        protected void Insert(T model)
        {
            MgClient.Insert<T>(model);
        }

        protected long Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return MgClient.Update<T>(filter, update);
        }

        protected List<T> Search(FilterDefinition<T> filter)
        {
            return MgClient.Search<T>(filter);
        }
        protected List<T> SearchByPage(FilterDefinition<T> filter, Expression<Func<T, object>> sort, bool isAsc, int pageIndex, int pageSize, out long total)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize > 0 ? pageSize : 12;

            return MgClient.Search<T>(filter,sort,isAsc,pageSize,pageIndex,out total);
        }

        protected List<T> Search()
        {
            return MgClient.Search<T>();
        }

        protected long Delete(FilterDefinition<T> filter)
        {
            return MgClient.Delete<T>(filter);
        }

        protected string Index(IndexKeysDefinition<T> indexKeys)
        {
            return MgClient.Index<T>(indexKeys);
        }

        protected List<BsonDocument> Aggregate(FilterDefinition<T> filter, ProjectionDefinition<T> group)
        {
            return MgClient.Aggregate(filter, group);
        }




        

    }
}
