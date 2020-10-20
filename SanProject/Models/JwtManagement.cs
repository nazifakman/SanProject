using SanProject.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SanProject.Models
{
    public static class JwtManager
    {
        /// <summary>
        /// Use the below code to generate symmetric Secret Key
        ///     var hmac = new HMACSHA256();
        ///     var key = Convert.ToBase64String(hmac.Key);
        /// </summary>
        private const string Secret = "UafmleVOUjTalJVzZuCuLUYbLyFoGgb7SlqSLW7gRMgLTqvsj/IN5vm5+epmO82KwBzaB1Cvxg1+i3GZCAJf6Q==";

        public static string GenerateToken(mdlUser user, int expireMinutes = 30)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("nm", user.FullName),
                            new Claim("ui", user.Id.ToString()),
                            new Claim("rl", ((short) user.UserType).ToString()
                            )
                        }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public static mdlUser GetUserFromToken(string token)
        {
            mdlUser user;
            var simplePrinciple = JwtManager.GetPrincipal(token);
            if (simplePrinciple == null)
                return null;
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return null;

            if (!identity.IsAuthenticated)
                return null;
            user = new mdlUser();
            var usernameClaim = identity.FindFirst("nm");
            user.FullName = usernameClaim.Value;
            usernameClaim = identity.FindFirst("ui");
            user.Id = Convert.ToInt32(usernameClaim.Value);
            usernameClaim = identity.FindFirst("rl");
            enmUserType role;
            Enum.TryParse<enmUserType>(usernameClaim.Value, out role);
            user.UserType= role;
            usernameClaim = identity.FindFirst("ah");
            if (string.IsNullOrEmpty(user.FullName))
                return null;
            return user;
        }
    }

}