using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.data
{
    /// <summary>
    /// 项目进度
    /// </summary>
    public class ProjectProcess
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string ProcessDesc { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public int UserID { get; set; }
        public string UserName { get; set; }


        public List<ProjectFlie> FlieList { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
    }
}
