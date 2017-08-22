﻿using System;
using System.Collections.Generic;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    /// <summary>
    /// 个人基本信息
    /// </summary>
    [BsonIgnoreExtraElements]
    public class MgPersonInfo : MgBaseModel
    {
        public string NickName { set; get; }
        public string RealName { set; get; }
        public string Birthday { set; get; }
        public string Gender { set; get; }
        //public string Age { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Position { set; get; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { set; get; }
        /// <summary>
        /// 工作年限
        /// </summary>
        public string WorkYear { set; get; }

        public int WorkYears { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UTime { set; get; }

        /// <summary>
        /// 评价平均分[信誉]
        /// </summary>
        public float EvalScore { set; get; }
    }
}
