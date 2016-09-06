using com.pmp.mongo.data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.service
{
    public class MgSolutionService : BaseService<MgSolution>
    {
        public void Init()
        {
            CreateCounter();
        }










        public MgSolution GetOneById(long id)
        {
            var filter = Builders<MgSolution>.Filter.Eq("ID", id);
            return Search(filter).FirstOrDefault();
        }



        public List<MgSolution> GetListByProId(int projectId)
        {
            var filter = Builders<MgSolution>.Filter.Eq("ProjectId", projectId);
            return Search(filter);
        }

    }
}
