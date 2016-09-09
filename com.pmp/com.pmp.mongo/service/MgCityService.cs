using com.pmp.mongo.data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.service
{
    public class MgCityService:BaseService<MgCity>
    {


        public List<MgCity> GetListByParentId(int parentId)
        {
            var filter = Builders<MgCity>.Filter.Eq("ParentId", parentId);
            return Search(filter);
        }























        

    }

}
