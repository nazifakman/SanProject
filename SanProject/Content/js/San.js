var Search = {
    criteria: {
        deparDate: null,
        returnDate: null,
        from: null,
        to: null,
        adult: 1,
        child: 0,
        infant: 0,
        oneway: false
    },
    obj: {
        locations: null
    },
    mounted: function () {
        this.ListRestaurant() // Calls the method before page loads
    },
    init: function () {
        
        Search.ListRestaurant();
        //$("#Kayit").click(Search.CreateUser);
        //$("#Giris").click(Search.loginUser);
    },
    ListRestaurant: function () {
        Search.apiGetPost("GET", "api/Restaurant/RestaurantList", null, Search.getRestaurant, null, null);
    },
    DeleteRestaurant: function () {
        Search.apiGetPost("GET", "api/Restaurant/DeleteRestaurant/"+id, null, Search.getRestaurant, null, null);
    },
    DeleteReviews: function () {
        Search.apiGetPost("GET", "api/DeleteReviews/" + id, null, Search.getRestaurant, null, null);
    },
    //loginUser: function () {

    //    var Mail = $("#Mail").val();
    //    var Sifre = $("#Sifre").val();

    //    var jsonVeri = {
    //        Email: Mail,
    //        Password: Sifre,

    //    };
    //    Search.apiGetPost("GET", "api/user/TryLoginMember", JSON.stringify(new { Email: jsonVeri.Email, Password: jsonVeri.Password }), Search.setUser, null, null);
    //},
    setUser: function (d) {
        var Profil = "";

        Profil += "<button class='btn btn-primary my-account d-none d-lg-block' data-toggle='modal' data-target='.bd-example-modal-lg2'>";
        Profil += " <i class='fa fa-user' aria-hidden='true'></i> " + d.Data.FullName;
        Profil += "    </button>";

        $("#UserProfile").append(Profil);
    },
    getRestaurant: function (th) {
        var test = "";

        $.each(th.Data, function (i, v) {

            test += "<div class='sin-coupon row no-gutters'>";
            test += "<div class='col-sm-9 col-lg-6 col-xl-7'>";
            test += "  <div class='coupon-content'>";

            test += "      <span class='ratestar'></span>";
            test += "     <div class='sav-coupon-wrap'>";
            test += "           <a class='sav-coupon' href='#' data-toggle='tooltip' data-placement='top' title='Save Coupon'>";
            test += "               " + v.RateStar + " <i class='fa fa-star-o' aria-hidden='true'></i>";
            test += "           </a>";
            test += "       </div>";
            test += "       <h2>" + v.Adi + "</h2>";

            test += "      <p>" + v.Description + "</p>";

            test += "   </div>";
            test += " </div>";
            test += "    <div class='col-sm-12 col-lg-6 col-xl-5'>";
            test += "      <div class='coupon-code'>";

            test += "         <div class='rate'>";
            test += "             <input type='radio' id='star5' name='rate' value='5' />";
            test += "             <label for='star5' title='text'>5 yıldız</label>";
            test += "             <input type='radio' id='star4' name='rate' value='4' />";
            test += "            <label for='star4' title='text'>4 yıldız</label>";
            test += "             <input type='radio' id='star3' name='rate' value='3' />";
            test += "           <label for='star3' title='text'>3 yıldız</label>";
            test += "           <input type='radio' id='star2' name='rate' value='2' />";
            test += "            <label for='star2' title='text'>2 yıldız</label>";
            test += "           <input type='radio' id='star1' name='rate' value='1' />";
            test += "           <label for='star1' title='text'>1 yıldız</label>";
            test += "      </div>";
            test += "        <textarea class='form-control' id='exampleFormControlTextarea1' style='margin-top: 0px; margin-bottom: 10px; margin-left: 20px; width: 80%; float: right'></textarea>";

            test += "       <button type='button' class='coupon-hcode' data-toggle='modal' data-target='.bd-example-modal-lg-product-1'>";
            test += "          <span class='hcode'>Değerlendir</span>";
            test += "       </button>";
            test += "     </div>";
            test += "  </div>";
            test +=" <div class='col-lg-12 col-xl-12' style='text-align:center' > <h4> Yorumlar</h4></div>"
            test += "       <div class='col-lg-12 col-xl-12' style='border-top:dashed 1px #000;margin-top:20px'></div>";
            test += "<div class='col-lg-12 col-xl-12'>"
            $.each(v.ReviewsList, function (i2, v2) {
                
                test += "       <div class='col-sm-3 col-lg-3 col-xl-3'>";
                test + "            <img src='./Content/img/review-author-img.png' alt='User' width='50'>";
                test += "       </div>";
                test += "           <div class='col-sm-9 col-lg-9 col-xl-9'>";
                test += "               <div class='coupon-content'>";
                test += "                   <h2>" + v2.FullName + " <i class='fa fa-star-o' aria-hidden='true' style='margin-left:30px'></i> " + v2.Score + "</h2>";
                test += "                    <p>" + v2.Comment + "</p>";

                test += "               </div>";
                test += "           </div>";
                
            })
            test += "                </div>";
            test += "                </div>";
            test += "       </div >";
            test += "</div >";

        })
        $("#result").append(test);
    },
    CreateUser: function () {
        var AdSoyad = $("#AdSoyad").val();
        var Mail = $("#Email").val();
        var Sifre = $("#Password").val();
        var Telefon = $("#Phone").val();
        var jsonData = {
            FullName: AdSoyad,
            Email: Mail,
            Password: Sifre,
            MobilePhone: Telefon
        };
        Search.apiGetPost("POST", "/api/user/CreateUser", JSON.stringify(jsonData), null, null, null);
    },
   
    formArrayToJson: function (formArray) {
        var returnArray = {};
        for (var i = 0; i < formArray.length; i++) {
            returnArray[formArray[i]['name']] = formArray[i]['value'];
        }
        return returnArray;
    },
    apiGetPost: function (getPost, absoluteUri, query, successFnc, errorFnc, complateFnc, async) {
        $.ajax({
            async: typeof (async) == "undefined" ? true : async,
            type: getPost,
            url: SitePath + "/" + absoluteUri,
            data: query,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: eval(successFnc),
            error: eval(errorFnc),
            complete: function (e) {
                if (e.statusText == 'OK' && complateFnc != null)
                    complateFnc();
            }
        });
    }
}