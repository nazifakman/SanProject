using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SanProject.Models;
using SanProject.Repository;


namespace SanProject.ApiControllers
{
    public class UserController : ApiController
    {
        public ApiResponse<int> CreateUser(mdlUser pUser)
        {
            int data;
            using (repoUser repo = new repoUser())
                data = repo.SaveUser(pUser);
            return new ApiResponse<int> { Data = data };
        }
        [HttpPost,HttpGet]
        public ApiResponse<mdlUser> TryLoginMember(mdlUser pUser)
        {

            
            using (repoUser repo = new repoUser())
                pUser = repo.SiteUserGet(pUser);

            string token = JwtManager.GenerateToken(pUser);
            pUser.Token = token;
            SessionObject.End();
            SessionObject.UserInfo = pUser;

            return new ApiResponse<mdlUser> { Data = pUser };
        }
    }

}
