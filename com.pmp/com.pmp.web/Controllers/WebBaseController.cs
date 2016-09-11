using com.pmp.mongo.data;
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
                if (user != null && user.Count > 0)
                    base._Login_CompanyReal_ID = user[0].CompanyReal_ID;

                if (user[0].Level == UserLevel.Person)
                {
                    base._Longin_Person_IsApprove = user[0].PersonReal.IsApprove;

                }
                else if (user[0].Level == UserLevel.CompanyAdmin || user[0].Level == UserLevel.CompanyUser)
                {
                    if (user[0].CompanyReal_ID > -1)
                    {
                        var comoany = new MgCompanyRealService().SearchById(user[0].CompanyReal_ID);
                        if (comoany != null && comoany.Count > 0)
                            base._Longin_CompanyReal_IsApprove = comoany[0].IsApprove;
                    }
                }
            }
        }

    }
}