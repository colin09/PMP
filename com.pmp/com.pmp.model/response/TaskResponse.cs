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
        public string Desc { get; set; }
        public int CreatesUserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AuditStatus { get; set; }
        public string AuditStatusDesc { get; set; }
        public int ReceiveUserId { set; get; }
        public DateTime CreateTime { get; set; }
        public int IsEvaluate_E { get; set; }
        public int IsEvaluate_I { get; set; }
        public List<ProjectFlie> FlieList { get; set; }
        public List<ProjectProcess> ProcessDesc { get; set; }
    }



}
