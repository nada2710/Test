
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineEducationPlatform.BLL.Manger.Accounts;
using OnlineEducationPlatform.DAL.Data.DbHelper;
using OnlineEducationPlatform.DAL.Data.Models;
using OnlineEducationPlatform.DAL.Repositories;
using System.Text;

namespace OnlineEducationPlatform.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //----------------------
            builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric=false;
                Options.Password.RequireLowercase=false;
                Options.Password.RequireUppercase=true;
               // Options.Password.RequiredLength=15;


            }).AddEntityFrameworkStores<EducationPlatformContext>();
           
            //-----------------------------------------------------
            //[authorize]
            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme="JWT";//make sure token is true
                Options.DefaultChallengeScheme="JWT";//return 401 => unauthorized or 403 => forbeden
            }).AddJwtBearer("JWT", Options =>
            {
                //secrete key
                var SecretKeyString = builder.Configuration.GetValue<string>("SecratKey");
                var SecreteKeyBytes = Encoding.ASCII.GetBytes(SecretKeyString);
                SecurityKey securityKey = new SymmetricSecurityKey(SecreteKeyBytes);
                //--------------------------------------------------------------

                Options.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey=securityKey,
                    //false mean anyone can send and eny one can take
                    ValidateIssuer=false,//take token(backend)//make token
                    ValidateAudience=false//send token(frontend)//use token
                };
            });




            builder.Services.AddDbContext<EducationPlatformContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });


            builder.Services.AddScoped<IAccountManger, AccountManger>();
           
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
