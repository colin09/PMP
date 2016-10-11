using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.data
{
    public class MgMessage : MgBaseModel
    {
        public int ID { set; get; }

        public int CreateUser { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { set; get; }

        public string Msg { set; get; }

        public int ToUserId { set; get; }

        public string ProjectCode { set; get; }

        public string Type { set; get; }
    }
}
