using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;

using Owin;
using System.Text;

[assembly:OwinStartup(typeof(JWT_token.Startup))]
namespace JWT_token
{
    public class Startup
    {

        public static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        byte[] bytes = Encoding.ASCII.GetBytes(Secret);
        public void Configuration(IAppBuilder app)
        {
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://mysite.com",
                    ValidAudience = "http://mysite.com",
                  IssuerSigningKey = new SymmetricSecurityKey(bytes)

                   
        }



            }) ; 
            
        
        }

    }
}