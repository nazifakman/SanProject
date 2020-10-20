using Dapper;
using SanProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanProject.Repository
{
    public class repoUser : BaseRepo
    {
        public mdlUser GetUser(mdlUser pUser)
        {
            string tSql = "select * from tblUser(nolock) where Email=@Email and Password=@Password";
            pUser = _db.QueryFirst<mdlUser>(tSql, pUser);
            return pUser;
        }
       
        public mdlUser GetUser(int pId)
        {
            mdlUser user;
            user = _db.Query<mdlUser>("select top 1 * from tbl_cms_userinfo (nolock) where Id=@Id", new { Id = pId }).FirstOrDefault();
            return user;
        }

        public int SaveUser(mdlUser pUser)
        {
            string tSql = "insert into tblUsers(FullName, Email, [Password], MobilePhone) values(@FullName, @Email, @Password, @MobilePhone)";
            int userId = _db.ExecuteScalar<int>(tSql, pUser);
            return userId;
            
        }
        
        public IEnumerable<mdlUser> GetUsers(int? pId)
        {
            return _db.Query<mdlUser>("select * from tbl_cms_userinfo (nolock) where Id=isnull(@Id,Id)", new { Id = pId });
        }
        public mdlUser SiteUserGet(mdlUser pUser)
        {
            string tsql = @"select * from tblUsers(nolock) where Email = @Email and [Password] = @Password";
            var user = _db.QueryFirstOrDefault<mdlUser>(tsql, pUser);

            //if (user != null && user.UserType == Models.enmUserType.Owner)
            //{
            //    tsql = @"select distinct CmsRoleId as RoleType from ida_tblCmsUserRoleCross(nolock) where CmsUserId=@Id";
            //    user.UserRoleTypes = _db.Query<enmUserType>(tsql, user);
                
            //}
            return user;
        }

    }
}
