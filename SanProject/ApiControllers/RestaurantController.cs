using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SanProject.Models;
using System.IO;
using Dapper;
using SanProject.Repository;


namespace SanProject.ApiControllers
{
    public class RestaurantController : ApiController
    {
        [HttpPost, HttpGet]
        public ApiResponse<List<mdlRestaurant>> RestaurantList()
        {
            List<mdlRestaurant> data;
            
            using (repoRestaurant repo = new repoRestaurant())
                data = repo.RestaurantList();
            
            return new ApiResponse<List<mdlRestaurant>> { Data = data };
        }

        public ApiResponse<int> AddRestaurant(mdlRestaurant pData)
        {
            int data;
            using (repoRestaurant repo = new repoRestaurant())
                data = repo.AddRestaurant(pData);
            return new ApiResponse<int> { Data = data };
        }

        public ApiResponse<int> DeleteRestaurant(int pId)
        {
            int data;
            using (repoRestaurant repo = new repoRestaurant())
                data = repo.DeleteRestaurant(pId);
            return new ApiResponse<int> { Data = data };
        }
    }
}
