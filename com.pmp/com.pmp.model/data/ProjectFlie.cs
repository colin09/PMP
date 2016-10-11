using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.data
{
    /// <summary>
    /// 项目文件
    /// </summary>
    public class ProjectFlie
    {
        /// <summary>
        /// 项目文件评价（立项说明、方案文件）
        /// [1.立项说明][2.方案文件]
        /// </summary>
        public int FileType { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int Index { set; get; }
    }
}
