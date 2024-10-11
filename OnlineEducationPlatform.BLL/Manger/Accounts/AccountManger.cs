using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineEducationPlatform.BLL.Dtos.ApplicationUserDto;
using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineEducationPlatform.BLL.Manger.Accounts
{
    public class AccountManger : IAccountManger
    {
        private readonly UserManager<ApplicationUser> UserManager;//make sure if user send in db or not
        private readonly IConfiguration configuration;

        public AccountManger(UserManager<ApplicationUser>userManager,IConfiguration configuration)//inject =>to use it
        {
            UserManager=userManager;
            this.configuration=configuration;
        }

        public async Task<AuthModel> Register(RegesterDto regesterDto)
        {
            //make sure email amd name is not in db

            if (await UserManager.FindByEmailAsync(regesterDto.Email) is not null)
            {
                return new AuthModel { message = "Email is already registered!" };
            }
            if (await UserManager.FindByNameAsync(regesterDto.UserName) is not null)
            {
                return new AuthModel { message = "Name is already registered!" };
            }

            ApplicationUser user=null;

            if (regesterDto.UserType == TypeUser.Student)
            {
                user = new Student();
            }
            else if (regesterDto.UserType == TypeUser.Instructor)
            {
                user = new Instructor();
            }
            else if(regesterDto.UserType == TypeUser.Admin)
            {
                user =new Admin();
            }

            user.Email = regesterDto.Email;
            user.UserName =regesterDto.UserName;
            user.PhoneNumber=regesterDto.PhoneNumber;
            user.UserType=regesterDto.UserType;

            IdentityResult identityResult = await UserManager.CreateAsync(user, regesterDto.Password);//save in db and hashing password

            if (!identityResult.Succeeded)
            {
                var Errors = string.Empty;
                foreach (var error in identityResult.Errors)
                {
                    Errors+=$"{error.Description},";
                }
                return new AuthModel { message = Errors };
            }

            var JwtSecurityToken = await CreateJwtToken(user);
            if (regesterDto.UserType == TypeUser.Student)
            {
                await UserManager.AddToRoleAsync(user, "STUDENT"); ;
            }
            else if (regesterDto.UserType == TypeUser.Instructor)
            {
                await UserManager.AddToRoleAsync(user, "INSTRUCTOR"); ;
            }
            else
            {
                await UserManager.AddToRoleAsync(user,"ADMIN");
            }

            return new AuthModel
            {
                Email=user.Email,
                ExpairationDate=JwtSecurityToken.ValidTo,
                IsAuthenticated=true,//if has creation in db
                Token=new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                UserName=user.UserName,
                
            };
        }


        public async Task<AuthModel> Login(LoginDto loginDto)
        {
            AuthModel auth = new AuthModel();

            var User = await UserManager.FindByEmailAsync(loginDto.Email);
            if (User == null|| !await UserManager.CheckPasswordAsync(User, loginDto.Password))
            {
                auth.message="Email or Password is incorrect";
                return auth;
            }

           
            var JwtSecurityToken = await CreateJwtToken(User);
            var rolelist = await UserManager.GetRolesAsync(User);

            auth.IsAuthenticated=true;
            auth.Token=new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            auth.Email=User.Email;
            auth.UserName=User.UserName;
            auth.ExpairationDate=JwtSecurityToken.ValidTo;
            auth.Roles=rolelist.ToList();

            return auth;

        }
       
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var UserClaims = await UserManager.GetClaimsAsync(user);
            var Roles = await UserManager.GetRolesAsync(user);
            var RoleClaims = new List<Claim>();
            foreach (var Rolename in Roles)
            {
                RoleClaims.Add(new Claim(ClaimTypes.Role, Rolename));
            }
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id)

            }.Union(UserClaims)
            .Union(RoleClaims);

            var SecretKeyString = configuration.GetSection("SecratKey").Value;
            var SecreteKeyBytes = Encoding.ASCII.GetBytes(SecretKeyString);
            SecurityKey securityKey = new SymmetricSecurityKey(SecreteKeyBytes);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);//(secrete key + hash algorithm)


            var Expiredate = DateTime.Now.AddDays(2);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims:Claims,
                signingCredentials:signingCredentials,
                expires: Expiredate
                );

            return jwtSecurityToken;

        }

       
    }
}
