using com.pmp.model.response;
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
        public List<MgCompanyReal> SearchById(int id)
        {
            var filter = Builders<MgCompanyReal>.Filter.Eq("ID", id);
            return Search(filter);
        }


        /// <summary>
        /// 查询公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MgCompanyReal> SearchByName(int name)
        {
            var filter = Builders<MgCompanyReal>.Filter.Eq("Name", name);
            return Search(filter);
        }


        /// <summary>
        /// 查询所有公司
        /// </summary>
        /// <returns></returns>
        public List<MgCompanyReal> SearchAll()
        {
            return Search();
        }

        /// <summary>
        /// 查询所有公司
        /// </summary>
        /// <returns></returns>
        public List<MgCompanyReal> SearchWhere(string phone, string contactsName, string companyName, PageInfo page, out long total)
        {
            var filter = Builders<MgCompanyReal>.Filter.Gt("status", -6);
            if (!string.IsNullOrWhiteSpace(phone))
                filter = filter & Builders<MgCompanyReal>.Filter.Eq(p => p.Phone, phone);
            if (!string.IsNullOrWhiteSpace(contactsName))
                filter = filter & Builders<MgCompanyReal>.Filter.Eq(p => p.ContactsName, contactsName);
            if (!string.IsNullOrWhiteSpace(companyName))
                filter = filter & Builders<MgCompanyReal>.Filter.Eq(p => p.Name, companyName);
            //List<MgCompanyReal> mgCompanyReal = Search();
            //if (!string.IsNullOrWhiteSpace(phone))
            //    mgCompanyReal = mgCompanyReal.FindAll(t => t.Phone == phone);
            //if (!string.IsNullOrWhiteSpace(contactsName))
            //    mgCompanyReal = mgCompanyReal.FindAll(t => t.ContactsName == contactsName);
            //if (!string.IsNullOrWhiteSpace(companyName))
            //    mgCompanyReal = mgCompanyReal.FindAll(t => t.Name == companyName);

            total = 0L;
            return SearchByPage(filter, order => order.CTime, false, page.PageIndex, page.PageSize, out total);
            // return mgCompanyReal;
        }

        /// <summary>
        /// 查询所有公司
        /// </summary>
        /// <returns></returns>
        public List<MgCompanyReal> SearchAllByAudit(int auditType, PageInfo page, out long total)
        {
            var filter = Builders<MgCompanyReal>.Filter.Gt("status", -6);
            if (auditType > 0)
            {
                filter = filter & Builders<MgCompanyReal>.Filter.Eq(p => p.IsApprove, auditType);
            }
            total = 0L;
            return SearchByPage(filter, order => order.CTime, false, page.PageIndex, page.PageSize, out total);

            // List<MgCompanyReal> mgCompanyReal = new List<MgCompanyReal>();
            //var list = Search();
            //foreach (var item in list)
            //{
            //    if (item.IsApprove == auditType)
            //    {
            //        mgCompanyReal.Add(item);
            //    }
            //}
            //return mgCompanyReal;
        }



        /// <summary>
        /// 更新公司账号状态
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool UpdateConpanyStatus(int conpanyID, int status)
        {
            var filter = Builders<MgCompanyReal>.Filter.Eq("ID", conpanyID);
            var update = Builders<MgCompanyReal>.Update.Set(u => u.status, status);
            return Update(filter, update) > 0;
        }


        public bool UpdateAudit(int Id, int auditType, string notpassstr)
        {
            var filter = Builders<MgCompanyReal>.Filter.Eq("ID", Id);
            var update = Builders<MgCompanyReal>.Update.Set(u => u.IsApprove, auditType).
                 Set(u => u.NotPassReason, notpassstr)
                .Set(u => u.ATime, DateTime.Now.ToString());
            return Update(filter, update) > 0;
        }

        public bool UpdateCompany(int companyID, MgCompanyReal mr)
        {
            var filter = Builders<MgCompanyReal>.Filter.Eq("ID", companyID);
            var update = Builders<MgCompanyReal>.Update.Set(u => u.IsApprove, mr.IsApprove).
                 Set(u => u.Name, mr.Name).
                 Set(u => u.Phone, mr.Phone).
                 Set(u => u.ContactsName, mr.Phone).
                 Set(u => u.CompanyAddress, mr.CompanyAddress).
                 Set(u => u.CompayCity, mr.CompayCity).
                 Set(u => u.RegistrID, mr.RegistrID).
                 Set(u => u.BuinessScope, mr.BuinessScope).
                 Set(u => u.OrganizationCode, mr.OrganizationCode).
                 Set(u => u.CompanyJustImg, mr.CompanyJustImg).
                 Set(u => u.CompanyAgainstImg, mr.CompanyAgainstImg).
                 Set(u => u.CUserID, mr.CUserID).
                 Set(u => u.IsApprove, mr.IsApprove).
                 Set(u => u.status, mr.status).
                 Set(u => u.NotPassReason, mr.NotPassReason)
                .Set(u => u.ATime, DateTime.Now.ToString());
            return Update(filter, update) > 0;
        }

        /// <summary>
        /// 添加认证
        /// </summary>
        /// <param name="mc"></param>
        public void CreateCompanyReal(MgCompanyReal mc, ref int id)
        {
            id = GetNewId();
            Insert(new MgCompanyReal()
            {
                ID = id,
                BuinessScope = mc.BuinessScope,
                CompanyAddress = mc.CompanyAddress,
                CompanyAgainstImg = mc.CompanyAgainstImg,
                CompayCity = mc.CompayCity,
                ContactsName = mc.ContactsName,
                Phone = mc.Phone,
                CompanyJustImg = mc.CompanyJustImg,
                CTime = mc.CTime,
                CUserID = mc.CUserID,
                status = mc.status,
                IsApprove = mc.IsApprove,
                Name = mc.Name,
                OrganizationCode = mc.OrganizationCode,
                RegistrID = mc.RegistrID,
                ATime = mc.ATime
            });
        }
    }
}
