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
    public class ReviewsController : ApiController
    {
        [HttpPost, HttpGet]
        public ApiResponse<List<mdlReviews>> ReviewsList()
        {
            List<mdlReviews> data;
            using (repoReviews repo = new repoReviews())
                data = repo.ReviewsList();
            return new ApiResponse<List<mdlReviews>> { Data = data };
        }
        [HttpPost, HttpGet]
        public ApiResponse<List<mdlReviews>> RestaurantReviewsList(int pId)
        {
            List<mdlReviews> data;
            using (repoReviews repo = new repoReviews())
                data = repo.RestaurantReviewsList(pId);
            return new ApiResponse<List<mdlReviews>> { Data = data };
        }
       [HttpPost, HttpGet]
        public ApiResponse<int> ReviewsAdd(mdlReviews pData)
        {
            int data;
            using (repoReviews repo = new repoReviews())
                data = repo.ReviewsSave(pData);

            return new ApiResponse<int>() { Data = data }; ;
        }
        [HttpPost, HttpGet]
        public ApiResponse<int> DeleteReviews(int pId)
        {
            int data;
            using (repoReviews repo = new repoReviews())
                data = repo.ReviewsDelete(pId);

            return new ApiResponse<int>() { Data = data }; ;
        }
    }
}
