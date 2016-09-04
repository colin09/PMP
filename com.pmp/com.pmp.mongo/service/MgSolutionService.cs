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


        public void Insert(int userId,int projectId, string desc, List<string> files)
        {
            var sln = new MgSolution() {
                ID = GetNewId(),
                ProjectId=projectId,
                SlnDesc = desc,
                FileList = files,
                UserId = userId,
            };

            Insert(sln);

        }








        public MgSolution GetOneById(long id)
        {
            var filter = Builders<MgSolution>.Filter.Eq("ID", id);
            return Search(filter).FirstOrDefault();
        }



        public MgSolution GetListByProId(int projectId)
        {
            var filter = Builders<MgSolution>.Filter.Eq("ProjectId", projectId);
            return Search(filter).FirstOrDefault();
        }

    }
}
