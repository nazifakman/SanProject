using System;
using System.Collections.Generic;
using System.Web;

namespace SanProject.Models
{
    public class mdlRestaurant
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
        
        public string OwnerName { get; set; }
        public decimal RateStar { get; set; }
        public List<mdlReviews> ReviewsList { get; set; }
    }
}