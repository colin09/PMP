using com.pmp.mongo.data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongodbs.data
{

    [BsonIgnoreExtraElements]
    public class MgUser : MgBaseModel
    {
        public string UserName { set; get; }
        public string UserPwd { set; get; }
        public string NickName { set; get; }
    }
}
