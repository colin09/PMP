using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.common.Config
{
    public abstract class BaseConfig
    {
        private NameValueCollection _collection;//= new NameValueCollection();

        protected abstract string SectionName { get; }

        protected string GetValue(string key)
        {
            //Console.WriteLine("key.getValue:" + key);
            if (_collection == null)
                _collection = GetConfiguration("", SectionName);
            //_collection = GetConfiguration("commonIntime", SectionName);

            //Console.WriteLine("key.count:" + _collection.Count);
            return _collection[key];
        }


        public NameValueCollection GetConfiguration(string groupName, string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentNullException(sectionName);

            if (!string.IsNullOrEmpty(groupName))
                groupName = groupName + "/";

            //Console.WriteLine("g-s:" + groupName + sectionName);
            return ConfigurationManager.GetSection(string.Concat(groupName, sectionName)) as NameValueCollection;
        }
    }
}
