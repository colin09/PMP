using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using com.pmp.mongo.data;

namespace com.pmp.mongo.service
{
    public class MgUserService : BaseService<MgUser>
    {
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


        public void CreateUser(string phone, string pwd, int userLevel)
        {
            Insert(new MgUser()
            {
                Phone = phone,
                Password = pwd,
                Level = (UserLevel)userLevel,
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
