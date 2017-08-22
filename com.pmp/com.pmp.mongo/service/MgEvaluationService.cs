using com.pmp.mongo.data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.service
{
    public class MgEvaluationService : BaseService<MgEvaluation>
    {



        public List<MgEvaluation> GetListByProId(int projectId)
        {
            var filter = Builders<MgEvaluation>.Filter.Eq("ProjectId", projectId);
            return Search(filter);
        }

        public List<MgEvaluation> GetListByUserId(int userId)
        {
            var filter = Builders<MgEvaluation>.Filter.Eq("UserId", userId);
            return Search(filter);
        }


        public List<MgEvaluation> GetListByRUserId(int userId)
        {
            var projects = new MgProjectService().GetListByRUser(userId);
            if (projects == null)
                return null;
            var proIds = projects.Select(p => p.ID).ToList();

            var filter = Builders<MgEvaluation>.Filter.In("ProjectId", proIds);
            filter = filter & Builders<MgEvaluation>.Filter.Ne("UserId", userId);
            return Search(filter);
        }

        public List<MgEvaluation> GetListByComUserId(int userId)
        {
            var projects = new MgProjectService().GetListByCUser(userId);
            if (projects == null)
                return null;
            var proIds = projects.Select(p => p.ID).ToList();

            var filter = Builders<MgEvaluation>.Filter.In("ProjectId", proIds);
            filter = filter & Builders<MgEvaluation>.Filter.Ne("UserId", userId);
            return Search(filter);
        }


        public float CalculateGradeAvg(int userId)
        {
            var query = _database.GetCollection<MgEvaluation>(new MgEvaluation().GetCollectionName()).Aggregate()
                .Match(e => e.ToUserId == userId)
                .Group(e => e.ToUserId, g => new
                {
                    count = g.Count(),
                    totalGrade = g.Sum(s => s.Grade),
                    totalScore = g.Sum(s => s.Score)
                });
            if (query.Any())
            {
                var m = query.FirstOrDefault();
                if (m.count > 0)
                    return m.totalGrade / (float)m.count;
            }
            return 0;
        }





    }
}
