using Dapper;
using SanProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanProject.Repository
{
    public class repoReviews : BaseRepo
    {

        public List<mdlReviews> ReviewsList ()
        {

            
            var Liste = _db.Query<mdlReviews>("select * From tblReviews").ToList();
            
            return Liste;

        }
        public List<mdlReviews> RestaurantReviewsList(int pId)
        {


            var Liste = _db.Query<mdlReviews>("select * From tblReviews Where RestId=PId",new { PId=pId}).ToList();

            return Liste;

        }
        public int ReviewsSave(mdlReviews pData)
        {
            try
            {
                
                string addSql = "insert into tblReviews (Score,Comment) values(@Score,@Comment);select SCOPE_IDENTITY() ";
                int q = _db.ExecuteScalar<int>(addSql, pData);
                string tsql = "insert into tblReviewsCross(ReviewsId,UserId,RestaurantId,MainId) values(@ReviewsId,@UserId,@RestaurantId,@MainId);";
                var sql = _db.ExecuteScalar<int>(tsql, new { ReviewsId = q, UserId = pData.UserId, RestaurantId = pData.RestaurantId, MainId = pData.MainId });
                return sql;
            }
            catch
            {
                return 0;
            }
        }
        public int ReviewsDelete(int pId)
        {
            string sql = "Delete tblReviews where Id =@PId";
            return _db.ExecuteScalar<int>(sql, new { PId = pId });

        }
    }
}
