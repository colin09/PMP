using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.request
{
    class TaskRequest
    {
    }



    public class TaskInfoReq
    {
        public int Id { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractCode { get; set; }
        /// <summary>
        /// 项目状态
        /// [1.待领取][2.待审核][3.进行中][4.待评价][5.已完成]
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 项目经理
        /// </summary>
        public string Manager { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public int CreatesUserID { get; set; }
        /// <summary>
        /// 项目开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 项目结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 项目预算
        /// </summary>
        public double Budget { set; get; }

        public int Province { set; get; }
        public int City { set; get; }
    }


}
