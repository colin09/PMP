using System;
using System.Collections.Generic;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;

namespace com.pmp.mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgCompanyReal : MgBaseModel
    {
        public MgCompanyReal()
        {
            CTime = DateTime.Now.ToOADate();
            UTime = DateTime.Now.ToOADate();
        }

        public int ID { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { set; get; }
        /// <summary>
        /// 营业执照注册号
        /// </summary>
        public string RegistrID { set; get; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string BuinessScope { set; get; }
        /// <summary>
        /// 组织机构代码证号
        /// </summary>
        public string OrganizationCode { set; get; }

        /// <summary>
        /// 组织机构代码证扫描件
        /// </summary>
        public string CompanyJustImg { set; get; }
        /// <summary>
        /// 营业执照副本扫描件
        /// </summary>
        public string CompanyAgainstImg { set; get; }

        public double CTime { set; get; }
        //[BsonDefaultValue((object)(DateTime.Now.ToOADate()))]
        public double UTime { set; get; }

    }


}
