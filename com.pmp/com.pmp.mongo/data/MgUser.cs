using System;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgUser : MgBaseModel
    {
       // public int Id { set; get; }
        public string UserName { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string CodePwd { set; get; }
        public DateTime CodePwdTime { set; get; }

        public UserRole Role { set; get; }
        public UserLevel Level { set; get; }

        public int Status { set; get; }
        public DateTime CTime { set; get; }
        public DateTime UTime { set; get; }


        public MgPersonInfo PersonInfo { set; get; }

        [BsonDefaultValue("0")]
        public int CompanyId { set; get; }



    }




    public class UserRole
    {
        public int RoleId { get; set; }
        public int RoleName { get; set; }
    }

    public enum UserLevel
    {
        Default = 0,
        Person = 1,
        CompanyAdmin,
        CompanyUser,
    }
}
