using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_token.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        private static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

        [HttpGet]
        public object GetToken()
        {//Access token
            var issuer = "http://mysite.com";
            var secretekey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
            var credentials = new SigningCredentials(secretekey, SecurityAlgorithms.HmacSha256);
            var permclaim = new List<Claim>();
            permclaim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permclaim.Add(new Claim("valid", "1"));
            permclaim.Add(new Claim("userid", "1"));
            permclaim.Add(new Claim("name", "surendra"));

            var token = new JwtSecurityToken(issuer, issuer, permclaim, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }
        [HttpPost]
        public string GetName1()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claim = identity.Claims;
                   
                }
                return "Valid";
            }
            else {
                return "Invalid";
            }
        
        }

        [Authorize]
        [HttpPost]
        public object GetName2()
        {
           
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claim = identity.Claims;
                    var name = claim.Where(p => p.Type == "name").FirstOrDefault()?.Value;
                    return new
                    {
                        data = name
                    };

                }
            return null;
              
            }
           

        }
    }

