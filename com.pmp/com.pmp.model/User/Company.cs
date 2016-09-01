using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model
{
    public class Model_Company
    {
        public int Company_ID { get; set; }

        /// <summary>
        ///审核状态
        ///[0.未审核][1.审核通过][2.审核失败]
        /// </summary>
        public string Company_AuditState { get; set; }

        /// <summary>
        ///公司状态
        ///[0.停用][1.启用]
        /// </summary>
        public string Company_State { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public int Company_Name { get; set; }
        /// <summary>
        ///营业执照注册号
        /// </summary>
        public string Company_Registr_ID { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Company_Address { get; set; }
        /// <summary>
        /// 营业年限
        /// </summary>
        public int Company_Year { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        public string Company_ManageFargoing { get; set; }
        /// <summary>
        ///住址机构代码证号
        /// </summary>
        public string Company_住址机构代码证号 { get; set; }

        /// <summary>
        ///组织机构代码证扫描件
        /// </summary>
        public string Company_组织机构代码证扫描件 { get; set; }
        /// <summary>
        ///联系电话
        /// </summary>
        public string Company_Mobile { get; set; }
        /// <summary>
        ///营业执照副本扫描件
        /// </summary>
        public string Company_营业执照副本扫描件 { get; set; }
    }
}
