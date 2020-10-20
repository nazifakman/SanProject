using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanProject.Models
{
    public class mdlReviews
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int Score { get; set; }

        //Sadece Veri Tabanı İçin
        public int ReviewsId { get; set; }
        public int RestaurantId { get; set; }
        public int UserId { get; set; }
        public int MainId { get; set; }
        public string FullName { get; set; }
        public decimal RateStar { get; set; }
    }
}