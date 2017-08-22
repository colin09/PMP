using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.model.response
{
    public class PageInfo
    {
        public PageInfo()
        {
            PageIndex = 1;
            PageSize = 12;
            Total = 0;
        }


        public int PageIndex { set; get; }
        public int PageSize { set; get; }
        public long Total { set; get; }
        public int PageCount
        {
            get
            {
                if (Total > 0 && PageSize > 0)
                    return (int)Math.Ceiling((double)Total / PageSize);
                return 1;
            }
        }
    }
}
