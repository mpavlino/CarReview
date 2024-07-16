using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;

namespace Review.Handlers {
    public class TokenHandler {
        private readonly IConfiguration _configuration;

        public TokenHandler( IConfiguration configuration ) {
            _configuration = configuration;
        }

        public string GenerateToken( string userId ) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes( _configuration["Jwt:Key"] );
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity( new Claim[]
                {
                new Claim(ClaimTypes.Name, userId)
                } ),
                Expires = DateTime.UtcNow.AddHours( 1 ),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ), SecurityAlgorithms.HmacSha256Signature )
            };
            var token = tokenHandler.CreateToken( tokenDescriptor );
            return tokenHandler.WriteToken( token );
        }
    }
}

