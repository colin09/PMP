using com.pmp.mongo.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.pmp.web.Controllers
{
    public class WebBaseController : com.pmp.common.mvc.ctl.BaseController
    {
        public WebBaseController()
        {
            if (_Longin_UserId > 0)
            {
                var user = new MgUserService().SearchById(_Longin_UserId);
                base._Login_CompanyReal_ID = user[0].CompanyReal_ID;
                base._Longin_Person_IsApprove = user[0].PersonReal.IsApprove;
                if (user[0].CompanyReal_ID > 0)
                {
                    var comoany = new MgCompanyRealService().SearchById(user[0].CompanyReal_ID);
                    base._Longin_CompanyReal_IsApprove = comoany[0].IsApprove;
                }
            }
        }

    }
}