using com.pmp.mongo.data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.service
{
   public class MgEvaluationService: BaseService<MgEvaluation>
    {



        public List<MgEvaluation> GetListByProId(int projectId)
        {
            var filter = Builders<MgEvaluation>.Filter.Eq("ProjectId", projectId);
            return Search(filter);
        }

        public List<MgEvaluation> GetListByUserId(int userId)
        {
            var filter = Builders<MgEvaluation>.Filter.Eq("CUser", userId);
            return Search(filter);
        }


        public List<MgEvaluation> GetListByRUserId(int userId)
        {
            var projects = new MgProjectService().GetListByRUser(userId);
            if (projects == null)
                return null;
            var proIds = projects.Select(p => p.ID).ToList();


            var filter = Builders<MgEvaluation>.Filter.In("ProjectId", proIds);
            return Search(filter);
        }



    }
}
