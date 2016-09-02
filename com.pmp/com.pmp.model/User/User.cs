using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model
{
    public class Model_User
    {
        public int User_ID { get; set; }
        /// <summary>
        /// 登录号
        /// </summary>
        public string User_LoginID { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string User_PassWord { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public int User_Rule_ID { get; set; }

        /// <summary>
        /// 所属公司
        /// </summary>
        public int User_Company_ID { get; set; }

        /// <summary>
        /// 个人信息
        /// </summary>
        public int User_UserInfo_ID { get; set; }

        /// <summary>
        /// 账户状态
        /// [0.未审核][1.审核通过][2.审核不通过]
        /// </summary>
        public int User_UserInfo_State { get; set; }
    }
    /// <summary>
    /// 个人信息
    /// </summary>
    public class Model_UserInfo {
        public int Model_UserInfo_ID { get; set; }
        public int Model_UserInfo_Name { get; set; }
        public int Model_UserInfo_Sex { get; set; }
        public int Model_UserInfo_Age { get; set; }
        public int Model_UserInfo_CardID { get; set; }
        public int Model_UserInfo_Mobile { get; set; }
        public int Model_UserInfo_Address { get; set; }
        /// <summary>
        /// 证件照片
        /// </summary>
        public int Model_UserInfo_Credentials { get; set; }

        /// <summary>
        /// 个人介绍
        /// </summary>
        public int Model_UserInfo_referral { get; set; }

        /// <summary>
        /// 技能
        /// </summary>
        public int Model_UserInfo_Skill { get; set; }
    }

    /// <summary>
    /// 角色
    /// </summary>
    public class Model_Role
    {
        public int Role_ID { get; set; }
        public int Role_Name { get; set; }
    }
}
