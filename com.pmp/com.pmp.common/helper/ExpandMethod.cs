using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using com.pmp.model.enums;

namespace com.pmp.common.helper
{
    public static class ExpandMethod
    {
        public static string Frmt(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        public static byte[] ToByte(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }


        public static T DesJson<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }


        public static string ToDate(this double oadate)
        {
            var date = DateTime.Parse("1899-12-30").AddMilliseconds(Math.Round(oadate * 24 * 60 * 60 * 1000));
            return date.ToString("yyyy-MM-dd");
        }



        public static string GetName(this ProjectCategroy category)
        {
            var name = "-";
            switch (category)
            {
                case ProjectCategroy.WireWith: name = "有线项目"; break;
                case ProjectCategroy.WireLess: name = "无线项目"; break;
                case ProjectCategroy.ServerMachine: name = "服务器"; break;
                case ProjectCategroy.DeviceSetup: name = "设备安装"; break;
                case ProjectCategroy.Evection: name = "出差"; break;
            }
            return name;
        }
        public static string GetName(this ProjectStatus status)
        {
            var name = "-";
            switch (status)
            {
                case ProjectStatus.Delete: name = "有线项目"; break;
                case ProjectStatus.Default: name = "待审核"; break;
                case ProjectStatus.Wait: name = "审核通过，等待领取"; break;
                case ProjectStatus.Action: name = "进行中"; break;
                case ProjectStatus.Over: name = "结束"; break;
                case ProjectStatus.Evaluation: name = "待评价"; break;
            }
            return name;
        }
        public static string GetName(this AuditStatus status)
        {
            var name = "-";
            switch (status)
            {
                case AuditStatus.Failure: name = "审核失败"; break;
                case AuditStatus.Default: name = "待审核"; break;
                case AuditStatus.Pass: name = "审核通过"; break;
            }
            return name;
        }
    }
}
