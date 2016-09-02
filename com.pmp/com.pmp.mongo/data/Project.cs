using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model
{
    /// <summary>
    /// 项目
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Model_Project : MgBaseModel
    {

        public Model_Project()
        {
            Project_FlieList = new List<Model_Project_Flie>();

            Project_PlanList = new List<Model_Project_Plan>();

            Project_EvaluateList = new List<Model_Project_Evaluate>();
        }
        /// <summary>
        /// 主键
        /// </summary>
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
        public int Manager { get; set; }
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
        public string StartTime { get; set; }
        /// <summary>
        /// 项目结束时间
        /// </summary>
        public string EndTime { get; set; }

        /// <summary>
        /// 审核状态
        /// [1.审核通过][0.未审核][2.审核未通过]
        /// </summary>
        public int AuditStatus { get; set; }
        /// <summary>
        /// 领取人项目方案
        /// </summary>
        public string GetProgram { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatesTime { get; set; }

        /// <summary>
        /// 雇主是否评价
        ///[0.未评价][1.已评价]
        /// </summary>
        public int IsEvaluate_E { get; set; }

        /// <summary>
        /// 实施是否评价
        /// [0.未评价][1.已评价]
        /// </summary>
        public int IsEvaluate_I { get; set; }





        /// <summary>
        /// 项目文件
        /// </summary>
        public List<Model_Project_Flie> Project_FlieList { get; set; }
        /// <summary>
        /// 项目进度
        /// </summary>
        public List<Model_Project_Plan> Project_PlanList { get; set; }
        /// <summary>
        /// 项目评价
        /// </summary>
        public List<Model_Project_Evaluate> Project_EvaluateList { get; set; }
    }




    /// <summary>
    /// 项目进度
    /// </summary>
    public class Model_Project_Plan
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Project_Plan_ID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int Project_ID { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// 
        public string Project_Plan_Desc { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public int Project_Plan_UserID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public int Project_Plan_CreateTime { get; set; }
    }

    /// <summary>
    /// 项目评价
    /// </summary>
    public class Model_Project_Evaluate
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Project_Evaluate_ID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int Project_ID { get; set; }

        /// <summary>
        /// 项目评价类型（雇主对实施人评价/实施人对雇主评价）
        /// [1.雇主评价][2.实施评价]
        /// </summary>
        public int Project_Evaluate_Type { get; set; }

        /// <summary>
        /// 评价描述
        /// </summary>
        public string Project_Evaluate_Desc { get; set; }

        /// <summary>
        /// 项目评分
        /// </summary>
        public double Project_Evaluate_Grade { get; set; }

        /// <summary>
        /// 评分级别【1-5】
        /// </summary>
        public double Project_Evaluate_Level { get; set; }
    }


    /// <summary>
    /// 项目文件
    /// </summary>
    public class Model_Project_Flie
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Project_Flie_ID { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int Project_ID { get; set; }

        /// <summary>
        /// 项目文件评价（立项说明、方案文件）
        /// [1.立项说明][2.方案文件]
        /// </summary>
        public int Project_Evaluate_Type { get; set; }

        /// <summary>
        /// 评价描述
        /// </summary>
        public int Project_Flie_Url { get; set; }
    }
}