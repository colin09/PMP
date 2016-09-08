using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.data
{
    public class MgCity : MgBaseModel
    {
        public int ID { set; get; }
        public int ParentId { set; get; }
        public string Name { set; get; }
        public string Remark { set; get; }
        public int Sort { set; get; }
    }
}
