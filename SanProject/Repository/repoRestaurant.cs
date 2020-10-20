using Dapper;
using SanProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanProject.Repository
{
    public class repoRestaurant : BaseRepo
    {

        public List<mdlRestaurant> RestaurantList()
        {

            
            var Liste = _db.Query<mdlRestaurant>("select * From tblRestaurant").ToList();
            foreach (var item in Liste)
            {
                string OwnerName = _db.Query<mdlUser>(" select * From tblRestaurant as res left join tblUsers as uzer on res.OwnerId=uzer.Id Where res.Id=@rId",new {rId=item.Id }).Select(x => x.FullName).FirstOrDefault();
                item.OwnerName = OwnerName;
                List<mdlReviews> reviews = _db.Query<mdlReviews>("  select rev.Id,rev.Comment,rev.Score,RestaurantId,UserId,FullName From tblReviewsCross as revc  left join tblReviews as rev on revc.ReviewsId=rev.Id left join tblUsers as uzer on  revc.UserId=uzer.Id  where RestaurantId=@Id", item).ToList();
                int ratesum = 0;
                for (int i = 0; i < reviews.Count; i++)
                {
                    ratesum += reviews[i].Score;
                }
                decimal ratestar = ratesum / reviews.Count;
                item.ReviewsList = reviews;
                item.RateStar = ratestar;
                
            }
            var newList = Liste.OrderByDescending(x => x.RateStar).ToList();
            return newList;

        }

        public int AddRestaurant(mdlRestaurant pData)
        {
            string sql = "INSERT INTO tblRestaurant(Adi,[Description],OwnerId) VALUES (@Adi,@Description,@OwnerId)";
            return _db.ExecuteScalar<int>(sql, pData);
            
        }
        public int DeleteRestaurant(int pId)
        {
            string sql = "Delete tblRestaurant where Id =@PId";
            return _db.ExecuteScalar<int>(sql, new { PId = pId });

        }

    }
}
