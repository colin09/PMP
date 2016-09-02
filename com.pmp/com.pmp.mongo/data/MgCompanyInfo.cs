using System;
using System.Collections.Generic;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgCompanyInfo : MgBaseModel
    {
        public MgCompanyInfo()
        {
            CTime = DateTime.Now.ToOADate();
            UTime = DateTime.Now.ToOADate();
        }

        public int Id { set; get; }
        public string Name { set; get; }
        public string ShortName { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string CardID { set; get; }
        /// <summary>
        /// 营业执照注册号
        /// </summary>
        public string RegistrID { set; get; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string BuinessScope { set; get; }
        /// <summary>
        /// 营业年限
        /// </summary>
        public int Years { set; get; }
        /// <summary>
        /// 组织机构代码证号
        /// </summary>
        public string OrganizationCode { set; get; }
        /// <summary>
        /// 证件照
        /// </summary>
        public List<string> Credentials { set; get; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Introduction { set; get; }

        public double CTime { set; get; }
        //[BsonDefaultValue((object)(DateTime.Now.ToOADate()))]
        public double UTime { set; get; }

    }


}
