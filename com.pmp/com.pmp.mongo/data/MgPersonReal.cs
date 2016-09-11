using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.data
{
    [BsonIgnoreExtraElements]

    public class MgPersonReal : MgBaseModel
    {
        public string RealName { set; get; }
        public string CardId { set; get; }
        public string Gender { set; get; }
        public string CardJustImg { set; get; }
        public string CardAgainstImg { set; get; }
        //职业
        public string  Profession { set; get; }
        public string Address { set; get; }

        /// <summary>
        /// 是否认证[提交1,审核通过2.审核不通过3.]
        /// </summary>
        public int IsApprove { set; get; }

        /// <summary>
        /// 不通过原因
        /// </summary>
        public string NotPassReason { get; set; }


        public string CreatesTime { get; set; }

        public string AuditTime { get; set; }
    }
}
