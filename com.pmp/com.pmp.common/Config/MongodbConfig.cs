
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.common.Config
{
    public class MongodbConfig : BaseConfig
    {

        public static MongodbConfig Current
        {
            get { return new MongodbConfig();}
        }

        public string Conn = "";

        public string DbName = "";




        protected override string SectionName { get { return "mongodb"; } }
    }
}
