using com.pmp.model.data;
using com.pmp.model.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.response
{
    public class TaskResponse
    {
    }

    public class TaskInfoRes
    {
        public int ID { get; set; }
        public int CategoryId { set; get; }
        public string CategoryName { set; get; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ContractCode { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Manager { get; set; }
        public string Linkman { get; set; }
        public string Mobile { get; set; }
        public string CityName { set; get; }
        public string ProvinceName { set; get; }
        public string Budget { set; get; }
        public string Desc { get; set; }
        public int CUserID { get; set; }
        public string CUserName { set; get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AuditStatus { get; set; }
        public string AuditStatusDesc { get; set; }
        public int RUserId { set; get; }
        public string RUserName { set; get; }
        public DateTime CTime { get; set; }
        public DateTime UTime { get; set; }
        public int CEvaluate { get; set; }
        public int PEvaluate { get; set; }
        public List<ProjectFlie> FlieList { get; set; }
        public List<TaskJoinRes> BidUsers { set; get; }

        public List<ProjectProcess> ProcessDesc { get; set; } = new List<ProjectProcess>();
    }


    public class TaskSlnRes
    {
        public int ID { set; get; }
        public int ProjectId { set; get; }
        public string ProjectName { get; set; }
        public string SlnDesc { set; get; }
        public List<ProjectFlie> FileList { set; get; }
        public int UserId { set; get; }
        public string UserName { set; get; }
        public DateTime CTime { set; get; }
        public DateTime UTime { set; get; }
        public int Result { set; get; }
    }


    public class TaskEvaRes
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int EvaluateType { get; set; }
        public int Grade { get; set; }
        public int Score { get; set; }
        public string Desc { get; set; }
        public int UserId { set; get; }
        public string UserName { set; get; }
        public DateTime CTime { set; get; }
    }



    public class TaskJoinRes
    {
        public int UserId { set; get; }
        public string UserName { set; get; }
        public DateTime CTime { set; get; }
        public DataStatus State { set; get; }

    }
}
