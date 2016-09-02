using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.data
{
    public class MgEvaluation : MgBaseModel
    {

        public int ProjectId { get; set; }

        /// <summary>
        /// 项目评价类型（雇主对实施人评价/实施人对雇主评价）
        /// [1.雇主评价][2.实施评价]
        /// </summary>
        public int EvaluateType { get; set; }

        /// <summary>
        /// 评价描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 项目评分
        /// </summary>
        public double Grade { get; set; }

        /// <summary>
        /// 评分级别【1-5】
        /// </summary>
        public double Level { get; set; }

        public double CTime { set; get; }
    }
}
