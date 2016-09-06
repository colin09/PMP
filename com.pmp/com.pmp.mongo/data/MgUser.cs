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
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CodePwdTime { set; get; }

        public UserRole Role { set; get; }
        public UserLevel Level { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UTime { set; get; }

        public MgPersonInfo PersonInfo { set; get; }

        public MgPersonReal PersonReal { get; set; }
        //公司ID
        public int CompanyReal_ID { get; set; }

        /// <summary>
        /// 默认1启用，0禁用
        /// </summary>
        public int Status { set; get; }
        

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
        CompanyAdmin = 2,
        ProjectManager = 3,
        CompanyUser = 4,
    }
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginUser
    {
        public string NickName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public UserLevel UserLevel { get; set; }
    }
}
