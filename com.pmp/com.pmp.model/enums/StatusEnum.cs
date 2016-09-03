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


    public enum ProjectStatus
    {
        //[1.待领取][2.待审核][3.进行中][4.待评价][5.已完成]

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
        Action,
        /// <summary>
        /// 结束
        /// </summary>
        Over,
        /// <summary>
        /// 带评价
        /// </summary>
        Evaluation,

    }

}
