using SanProject.Models;
using System;
using System.Collections.Generic;
using System.Web;
using SanProject.Repository;
using System.Web.Http;



namespace SanProject.Models
{
    public static class SessionObject
    {
        public static mdlUser UserInfo
        {
            get
            {
                return (mdlUser)HttpContext.Current.Session["UserInfo"];
            }
            set
            {
                HttpContext.Current.Session["UserInfo"] = value;
            }
        }
        public static void End()
        {
            if (System.Web.HttpContext.Current != null)
            {
                int cookieCount = System.Web.HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = System.Web.HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new System.Web.HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        System.Web.HttpContext.Current.Response.Cookies.Add(expiredCookie);
                    }
                }

                // clear cookies server side
                System.Web.HttpContext.Current.Request.Cookies.Clear();
            }

        }
    }
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public ApiResponse()
        {
            Status = true;
            Message = "İşlem Başarılı";
        }
    }
    
}