using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.enums
{
    public class StatusEnum
    {
    }



    public enum AuditStatus
    {
        Failure = -1,
        Default = 0,
        Pass = 1
    }
    public enum DataStatus
    {
        Delete = -1,
        Default = 0,
        Normal = 1
    }





    public enum ProjectStatus
    {
        Delete = -6,

        /// <summary>
        /// 待接单
        /// </summary>
        Default = 0,
        /// <summary>
        /// 待服务，选定用户
        /// </summary>
        Wait = 1,
        /// <summary>
        /// 服务中，已提交实施方案
        /// </summary>
        Action = 2,
        /// <summary>
        /// 完成，待验收
        /// </summary>
        Audit = 3,
        /// <summary>
        /// 验收通过，待评价
        /// </summary>
        Evaluation = 4,
        /// <summary>
        /// 项目完成
        /// </summary>
        Over = 5,


    }

    public enum ProjectStatus1
    {
        //[1.待领取][2.待审核][3.进行中][4.待评价][5.已完成]

        Delete = -6,

        /// <summary>
        /// 待审核
        /// </summary>
        Default = 0,
        /// <summary>
        /// 审核通过，带领取
        /// </summary>
        Wait = 1,
        /// <summary>
        /// 进行中
        /// </summary>
        Action = 2,
        /// <summary>
        /// 结束
        /// </summary>
        Over = 3,
        /// <summary>
        /// 带评价
        /// </summary>
        Evaluation = 4,

    }

}
