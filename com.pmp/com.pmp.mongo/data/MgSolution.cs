using com.pmp.model.data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.data
{
    public class MgSolution : MgBaseModel
    {
        public MgSolution()
        {
            FileList = new List<ProjectFlie>();
        }

        public int ID { set; get; }

        public int ProjectId { set; get; }

        public string SlnDesc { set; get; }

        public List<ProjectFlie> FileList { set; get; }

        public int UserId { set; get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CTime { set; get; } = DateTime.Now;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UTime { set; get; } = DateTime.Now;

        public int Result { set; get; } = 0;



    }
}
