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


        public string FromPlace { set; get; }

        public string ServerPlace { set; get; }

        public int WayHours { set; get; }

        public string ServerType { set; get; }

        public DateTime StartTime { set; get; }
        public DateTime EndTime { set; get; }

        public int ServerHours
        {
            set { }
            get
            {
                try
                {
                    return (EndTime - StartTime).Hours;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string ProcessDesc { get; set; }

        /// <summary>
        /// 遗留问题
        /// </summary>
        public string Nodus { set; get; }

        /// <summary>
        /// 维护建议
        /// </summary>
        public string Suggest { set; get; }


    }
}
