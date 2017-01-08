using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockServer.TokenProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer
{
    public partial class Startup
    {
        private void ConfigureAuth(IApplicationBuilder app)
        {
            app.UseIdentity();
            ConfigureTokenAuth(app);
        }

        private void ConfigureTokenAuth(IApplicationBuilder app)
        {
            //TODO: перенести ключь в конфиги
            var secretKey = "mysupersecret_secretkey!123";
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = false,
                //ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = false,
                //ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AuthenticationScheme = "JwtAuth",
                AutomaticAuthenticate = false,
                AutomaticChallenge = false,
                TokenValidationParameters = tokenValidationParameters
            });

            var options = new TokenProviderOptions
            {
                //Audience = "ExampleAudience",
                //Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
        }
    }
}
