using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using com.pmp.mongo.data;

namespace com.pmp.mongo.service
{
    public class MgUserService : BaseService<MgUser>
    {

        public void Init()
        {
            CreateCounter();
        }

        public List<MgUser> SearchById(long id)
        {
            var filter = Builders<MgUser>.Filter.Eq("Id", id);
            return Search(filter);
        }

        /// <summary>
        /// 验证是否存在电话账号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<MgUser> SearchLogin(string code)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", code);
            return Search(filter);
        }

        /// <summary>
        /// 查看所有用户
        /// </summary>
        /// <returns></returns>
        public List<MgUser> SearchAll()
        {
            return Search();
        }



        public bool CreateCodePwd(string phone)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", phone);
            var update = Builders<MgUser>.Update.Set(u => u.CodePwd, "").Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }
        //修改个人信息
        public bool UpdateAccountInfo(string phone, MgPersonInfo mp)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", phone);
            var update = Builders<MgUser>.Update.Set(u => u.PersonInfo, mp)
                .Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }

        //提交个人认证
        public bool UpdateAccountApprove(string phone, MgPersonReal mp)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", phone);
            var update = Builders<MgUser>.Update.Set(u => u.PersonReal, mp)
                .Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }

        //提交企业认证并且增加公司
        public bool UpdateAccountCompanyReal(string phone, MgCompanyReal mp)
        {
            int companyID = 0;
            new MgCompanyRealService().CreateCompanyReal(mp, ref companyID);

            var filter = Builders<MgUser>.Filter.Eq("Phone", phone);
            var update = Builders<MgUser>.Update.Set(u => u.CompanyReal_ID, companyID)
                .Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }

        public void CreateUser(string phone, string pwd, int userLevel)
        {
            MgPersonReal mp = new MgPersonReal();
            if ((UserLevel)userLevel == UserLevel.Person)
            {
                mp.IsApprove = 0;
            }

            Insert(new MgUser()
            {
                ID = GetNewId(),
                Status = 1,
                Phone = phone,
                Password = pwd,
                Level = (UserLevel)userLevel,
                AccountType = (UserLevel)userLevel,
                CTime = DateTime.Now,
                UTime = DateTime.Now,
                PersonReal = mp
            });
        }


        public MgUser Login(string phone, string pwd, string codePwd)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", phone);
            var user = Search(filter).FirstOrDefault();
            if (user != null)
            {
                if (!string.IsNullOrEmpty(pwd) && user.Password == pwd)
                    return user;
                if (!string.IsNullOrEmpty(codePwd) && user.CodePwd == codePwd)
                    return user;
            }
            return null;
        }


    }
}
