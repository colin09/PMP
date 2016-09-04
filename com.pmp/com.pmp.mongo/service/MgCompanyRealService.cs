using com.pmp.mongo.data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.pmp.mongo.service
{
    public class MgCompanyRealService : BaseService<MgCompanyReal>
    {
        /// <summary>
        /// 查询公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MgCompanyReal> SearchById(int  id)
        {
            var filter = Builders<MgCompanyReal>.Filter.Eq("ID", id);
            return Search(filter);
        }

        /// <summary>
        /// 添加认证
        /// </summary>
        /// <param name="mc"></param>
        public void CreateCompanyReal(MgCompanyReal mc,ref int id)
        {
            id = GetNewId();
            Insert(new MgCompanyReal()
            {
                ID = id,
                Address = mc.Address,
                BuinessScope = mc.BuinessScope,
                CompanyAddress = mc.CompanyAddress,
                CompanyAgainstImg = mc.CompanyAgainstImg,
                CompanyJustImg = mc.CompanyJustImg,
                CTime = mc.CTime,
                CUserID = mc.CUserID,
                IsApprove = mc.IsApprove,
                Name = mc.Name,
                OrganizationCode = mc.OrganizationCode,
                RegistrID = mc.RegistrID,
                UTime = mc.UTime
            });
        }

    }
}
