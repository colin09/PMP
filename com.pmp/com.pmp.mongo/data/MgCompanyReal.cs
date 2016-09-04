﻿using System;
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

        public double UTime { set; get; }
        /// <summary>
        ///创建用户
        /// </summary>
        public int CUserID { get; set; }
        /// <summary>
        /// 是否认证[默认0,提交1,审核通过2.审核不通过3.]
        /// </summary>
        public int IsApprove { set; get; }

    }


}
