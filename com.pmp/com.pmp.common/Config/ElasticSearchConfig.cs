using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.common.Config
{
    public class ElasticSearchConfig : BaseConfig
    {
        public static ElasticSearchConfig Current
        {
            get { return new ElasticSearchConfig();}
        }


        public string Host { get { return GetValue("Host"); } }
        
        public string Index { get { return GetValue("Index"); } }
        public string IndexPrefix { get { return GetValue("IndexPrefix"); } }

        public const string Ik_Max_Word = "ik_max_word";

        public const string Ik_Smart = "ik_smart";

        protected override string SectionName { get { return "elasticSearch"; } }




    }
}
