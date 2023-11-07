using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using TokenBlackListService;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DbEntityContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DbEntityContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("/register")]
        public async Task<ActionResult<User>> Register(String Username, string Userpassword,int privilege)
        {   
            
            
            if( Username==null || Userpassword==null || privilege==0){
                return BadRequest("Veuillez saisir tous les identifiants");
            }
            else{
                if(privilege==1 || privilege==2 || privilege==3){
                    var nameUserChecked= _context.user.Where(u => u.Name == Username).FirstOrDefault();
                    Console.WriteLine(nameUserChecked);
                    if(nameUserChecked==null){
                            string salt = BCrypt.Net.BCrypt.GenerateSalt();
                            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Userpassword,salt);
                            User user= new User();
                            user.Name=Username;
                            user.Password = hashedPassword;
                            user.Privilege=privilege;


                            _context.Add(user);
                            await _context.SaveChangesAsync();
                            return user;
                    }
                    else{
                            return BadRequest("Ce nom d'utilisateur est déjà utilisé");
                    }
                }
                
                else{
                        return BadRequest("Le privilege Accordé ne correspend a aucun privilège");
                }
            }
            
        }


        [HttpPost("/login")]
        public async Task<ActionResult> LogIn(string Username,string Userpw)
        {
            var user = await _context.user.FirstOrDefaultAsync(u => u.Name == Username);

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(Userpw, user.Password))
                {
                    // Authentification réussie
                    string token = GenerateJWT(user);
                    return Ok("Voici le token:"+token);
                }
                return BadRequest("Mot de passe incorrect.") ;
            }
            else
            {
                return BadRequest("Nom d'utilisateur introuvable.") ;
            }
           
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {   
            var tokenBlacklist= new TokenBlacklist();
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            tokenBlacklist.RevokeToken(token);
            return Ok("Vous êtes déconnecté");
        }
        [HttpPost("/refresh-token")]
        public IActionResult RefreshToken(User user)
        {
            var token=GenerateJWT(user);
            return Ok(token);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenerateJWT(User user){
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name)
            };

            //Secrete key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Settings:JWTSecret").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(240);

            var token = new JwtSecurityToken(
                claims:claims,
                expires: expiration,
                signingCredentials: creds
            );
            var jwt=new JwtSecurityTokenHandler().WriteToken(token) ;
            return jwt;
        }

        

    }
}
