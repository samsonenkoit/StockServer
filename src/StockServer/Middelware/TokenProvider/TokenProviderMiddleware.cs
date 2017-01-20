using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StockServer.BL.DataProvider.Interface;
using StockServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StockServer.TokenProvider
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenProviderOptions _options;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPointTransactionProvider _pointProvider;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IPointTransactionProvider pointProvider,
            IOptions<TokenProviderOptions> options, UserManager<ApplicationUser> signInManager)
        {
            _next = next;
            _options = options.Value;
            _userManager = signInManager;
            _pointProvider = pointProvider;
        }

        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.Path, StringComparison.Ordinal))
            {
                return _next(context);
            }

            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!context.Request.Method.Equals("POST")
               || !context.Request.HasFormContentType)
            {
                context.Response.StatusCode = 400;
                return context.Response.WriteAsync("Bad request.");
            }

            return GenerateToken(context);
        }

        private async Task GenerateToken(HttpContext context)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var user = await ValidateUser(username, password);
            if (user == null)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }
            
            DateTimeOffset now = DateTime.Now;

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            // You can add other claims here, if you want:
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };


            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now.DateTime,
                expires: now.DateTime.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };

            //TODO: Вынести начисление баллов из провайдера токенов
            var pointTask = _pointProvider.EnrollmentPointsForAuthorizationIfNeedAsync(user.Id);

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ApplicationUser> ValidateUser(string username, string password)
        {

            var user = await _userManager.FindByNameAsync(username).ConfigureAwait(false);

            if(user == null)
                return await Task.FromResult<ApplicationUser>(null);

            var isValid = await _userManager.CheckPasswordAsync(user, password).ConfigureAwait(false);

            if(!isValid)
                return await Task.FromResult<ApplicationUser>(null);

            // return await Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));
            return user;
        }

    }
}
