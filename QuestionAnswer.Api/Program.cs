using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using QuestionAnswer.DTO.Services;
using QuestionAswer.ADO;

namespace QuestionAnswer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IDataBaseService, DataBase>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        // ���������, ����� �� �������������� �������� ��� ��������� ������
                        ValidateIssuer = true,
                        // ������, �������������� ��������
                        ValidIssuer = AuthOptions.ISSUER,
                        // ����� �� �������������� ����������� ������
                        ValidateAudience = true,
                        // ��������� ����������� ������
                        ValidAudience = AuthOptions.AUDIENCE,
                        // ����� �� �������������� ����� �������������
                        ValidateLifetime = true,
                        // ��������� ����� ������������
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // ��������� ����� ������������
                        ValidateIssuerSigningKey = true,
                    };
                });

            //builder.WebHost.UseUrls("http://127.0.0.1:8080");

            var app = builder.Build();

            
            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}