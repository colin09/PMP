using System;
using System.Collections.Generic;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgPersonInfo : MgBaseModel
    {
        public string NickName { set; get; }
        public string RealName { set; get; }
        public string Birthday { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string CardID { set; get; }
        /// <summary>
        /// 证件照
        /// </summary>
        public List<string> Credentials { set; get; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { set; get; }
        /// <summary>
        /// 技能
        /// </summary>
        public string Skill { set; get; }

        public DateTime CTime { set; get; }
        public DateTime UTime { set; get; }

    }


}
