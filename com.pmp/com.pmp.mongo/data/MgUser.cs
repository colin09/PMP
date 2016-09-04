using System;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgUser : MgBaseModel
    {
        public int ID { set; get; }
        public string UserName { set; get; }
        public string Phone { set; get; }
        public string Password { set; get; }
        public string CodePwd { set; get; }
        public DateTime CodePwdTime { set; get; }

        public UserRole Role { set; get; }
        public UserLevel Level { set; get; }
        
        public DateTime CTime { set; get; }
        public DateTime UTime { set; get; }

        public MgPersonInfo PersonInfo { set; get; }

        public MgPersonReal PersonReal { get; set; }

        //public MgCompanyReal CompanyReal { get; set; }
        //公司ID
        public int CompanyReal_ID { get; set; }

        /// <summary>
        /// 默认1启用，0禁用
        /// </summary>
        public int Status { set; get; }

        //注册类型
        public UserLevel AccountType { set; get; }
       
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
