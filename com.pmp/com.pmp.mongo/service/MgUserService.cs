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
                .Set(u => u.IsApprove, 1)
                .Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }

        //提交企业认证
        public bool UpdateAccountCompanyReal(string phone, MgCompanyReal mp)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", phone);
            var update = Builders<MgUser>.Update.Set(u => u.CompanyReal, mp)
                .Set(u => u.IsApprove, 1)
                .Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }

        public void CreateUser(string phone, string pwd, int userLevel)
        {
            Insert(new MgUser()
            {
                ID = GetNewId(),
                Status = 1,
                Phone = phone,
                Password = pwd,
                Level = (UserLevel)userLevel,
                AccountType = userLevel,
                CTime = DateTime.Now,
                UTime = DateTime.Now
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
