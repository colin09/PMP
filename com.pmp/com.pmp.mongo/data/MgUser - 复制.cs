using System;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgPersonInfo : MgBaseModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Password { set; get; }
        public string CodePwd { set; get; }
        public DateTime CodePwdTime { set; get; }
        public string NickName { set; get; }
        public string Phone { set; get; }
        

        public DateTime CTime { set; get; }
        public DateTime UTime { set; get; }

    }


    public enum UserLevel1
    {
        Default=0,
        Person=1,
        Company,

    }
}
