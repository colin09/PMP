using com.pmp.model.data;
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
        public string Desc { get; set; }
        public int CUserID { get; set; }
        public string CUserName { set; get; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AuditStatus { get; set; }
        public string AuditStatusDesc { get; set; }
        public int RUserId { set; get; }
        public string RUserName { set; get; }
        public DateTime CreateTime { get; set; }
        public int CEvaluate{ get; set; }
        public int PEvaluate { get; set; }
        public List<ProjectFlie> FlieList { get; set; }
        public List<ProjectProcess> ProcessDesc { get; set; }
    }



}
