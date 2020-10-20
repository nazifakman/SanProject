using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanProject.Models
{
    public class mdlUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public enmUserType UserType { get; set; }
        public IEnumerable<enmUserType> UserRoleTypes { get; set; }
        public string Token { get; set; }
    }
}