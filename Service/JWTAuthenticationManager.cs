using ENTITYAPP.Service.Contract;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using GeeksConfiguration;
using ENTITYAPP.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace ENTITYAPP.Service
{
    
    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {

      

       

      

        private readonly IConfigManager _configuration;
        private readonly EmployeeDbContext _context;



        private readonly string tokenKey;

        
 
        public JWTAuthenticationManager( IConfigManager configuration,EmployeeDbContext context)
        {
            _configuration = configuration;

            _context = context;

           // _logger = logger;
          

            this.tokenKey = _configuration.TokenKey;
        }
 
        public string Authenticate(string email, string password)
        {


            //_logger.LogInformation("this is info");

            var user = _context.Users.FirstOrDefault(u => u.email == email && u.password == password);

            if (user==null)
            {
                return null;
            }
 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
 }
