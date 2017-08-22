using System.Configuration;

namespace com.pmp.common.Config
{
    public class AppSettingConfig
    {


        public static string TDES_Key
        {
            get { return ConfigurationManager.AppSettings["TDES_Key"]; }
        }
        public static string TDES_IV
        {
            get { return ConfigurationManager.AppSettings["TDES_IV"]; }
        }


        public static string MgConn
        {
            get { return ConfigurationManager.AppSettings["MgConn"]; }
        }
        public static string MgDBName
        {
            get { return ConfigurationManager.AppSettings["MgDBName"]; }
        }
        public static string MgPrefix
        {
            get { return ConfigurationManager.AppSettings["MgPrefix"]; }
        }


        public static string MqHost
        {
            get { return ConfigurationManager.AppSettings["MqHost"]; }
        }
        public static string MqUser
        {
            get { return ConfigurationManager.AppSettings["MqUser"]; }
        }

        public static string MqPwd
        {
            get { return ConfigurationManager.AppSettings["MqPwd"]; }
        }

        public static string MqExchange
        {
            get { return ConfigurationManager.AppSettings["MqExchange"]; }
        }
        public static string MqQueue
        {
            get { return ConfigurationManager.AppSettings["MqQueue"]; }
        }





    }
}
