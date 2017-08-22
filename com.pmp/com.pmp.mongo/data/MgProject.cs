using com.pmp.model.data;
using com.pmp.model.enums;
using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.data
{
    /// <summary>
    /// 项目
    /// </summary>
    [BsonIgnoreExtraElements]
    public class MgProject : MgBaseModel
    {

        public MgProject()
        {
            FlieList = new List<ProjectFlie>();

            ProcessDesc = new List<ProjectProcess>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { get; set; }
        public ProjectCategroy Category { set; get; }
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
        public ProjectStatus Status { get; set; }
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
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 项目结束时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 审核状态
        /// [1.审核通过][0.未审核][2.审核未通过]
        /// </summary>
        public AuditStatus AuditStatus { get; set; }
        /// <summary>
        /// 项目 执行人
        /// </summary>
        public int ReceiveUserId { set; get; }
        /// <summary>
        /// 领取人项目方案
        /// </summary>
        //public string GetProgram { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { get; set; }

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

        public string Budget { set; get; }

        public int ProvinceId { set; get; }
        public int CityId { set; get; }

        /// <summary>
        /// 参与竞标用户
        /// </summary>
        public List<BidUser> BidUsers { set; get; }

        /// <summary>
        /// 项目文件
        /// </summary>
        public List<ProjectFlie> FlieList { get; set; }
        /// <summary>
        /// 项目进度
        /// </summary>
        public List<ProjectProcess> ProcessDesc { get; set; }
    }





    public class BidUser
    {
        public int UserId { set; get; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { set; get; }
        public DataStatus Status { set; get; }
    }









}