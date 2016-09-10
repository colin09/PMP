﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using com.pmp.mongo.data;
using com.pmp.model.response;

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
            var filter = Builders<MgUser>.Filter.Eq("ID", id);
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
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<MgUser> SearchWhere(string name, string phone)
        {
            var filter = Builders<MgUser>.Filter.Eq("CompanyReal_ID", 0);
            List<MgUser> list = Search(filter);

            if (!string.IsNullOrWhiteSpace(name))
            {
                list = list.FindAll(t => t.PersonInfo.RealName == name);
            }
            if (!string.IsNullOrWhiteSpace(phone))
            {
                list = list.FindAll(t => t.Phone == phone);
            }
            return list;
        }

        /// <summary>
        /// 根据公司ID获取员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<MgUser> SearchuSserByCompanyId(int companyId, string name, string phone)
        {
            List<MgUser> userList = new List<data.MgUser>();
            List<MgCompanyReal> companyList = new MgCompanyRealService().SearchById(companyId);
            if (companyList != null && companyList.Count > 0)
            {
                var filter = Builders<MgUser>.Filter.Eq("CompanyReal_ID", companyList[0].ID);
                userList = Search(filter);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    userList = userList.FindAll(t => t.PersonInfo.RealName == name);
                }
                if (!string.IsNullOrWhiteSpace(phone))
                {
                    userList = userList.FindAll(t => t.Phone == phone);
                }
            }
            return userList;
        }


        public List<SimpleUserRes> GetUserListByIds(List<int> list)
        {
            if (list == null || list.Count < 1)
                return null;
            var filter = Builders<MgUser>.Filter.In("ID", list);
            return Search(filter).Select(m => new SimpleUserRes()
            {
                Id = m.ID,
                Name = m.PersonInfo.RealName
            }).ToList();

        }

        #region 用户审核相关
        /// <summary>
        /// 根据审核状态查询所有用户
        /// </summary>
        /// <returns></returns>
        public List<MgUser> SearchAllByAudit(int auditType, int accountType)
        {
            List<MgUser> mgUser = new List<MgUser>();
            if (accountType == 1)
            {
                var list = Search();
                foreach (var item in list)
                {
                    if (item.PersonReal != null)
                    {
                        if (item.PersonReal.IsApprove == auditType)
                        {
                            mgUser.Add(item);
                        }
                    }
                }
            }
            else
            {
                var list = new MgCompanyRealService().SearchAllByAudit(auditType);
                foreach (var item in list)
                {
                    mgUser.AddRange(SearchById(item.CUserID));
                }
            }
            return mgUser;
        }

        /// <summary>
        /// 更新审核状态
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="accountType"></param>
        /// <returns></returns>
        public bool UpdateAudit(int Id, int auditType, string notpassstr)
        {
            var filter = Builders<MgUser>.Filter.Eq("ID", Id);
            var update = Builders<MgUser>.Update.Set(u => u.PersonReal.IsApprove, auditType).
                Set(u => u.PersonReal.NotPassReason, notpassstr).
                Set(u => u.PersonReal.AuditTime, DateTime.Now.ToString());
            return Update(filter, update) > 0;
        }


        #endregion


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
                .Set(u => u.CompanyReal_Name, mp.Name)
                .Set(u => u.CodePwdTime, DateTime.Now);
            return Update(filter, update) > 0;
        }

        public void CreateUser(string phone, string pwd, int userLevel)
        {
            MgPersonReal mp = null;
            MgPersonInfo mi = null;
            if ((UserLevel)userLevel == UserLevel.Person)
            {
                mp = new MgPersonReal();
                mp.IsApprove = 0;
                mi = new data.MgPersonInfo();
            }

            Insert(new MgUser()
            {
                ID = GetNewId(),
                Status = 1,
                Phone = phone,
                Password = pwd,
                Level = (UserLevel)userLevel,
                CTime = DateTime.Now,
                UTime = DateTime.Now,
                PersonReal = mp,
                PersonInfo = mi
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
