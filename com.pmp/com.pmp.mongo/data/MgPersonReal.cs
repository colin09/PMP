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
        public int Profession { set; get; }
        public string Address { set; get; }

    }
}
