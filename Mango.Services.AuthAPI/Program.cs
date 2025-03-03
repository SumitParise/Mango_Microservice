
using Mango.Services.AuthAPI.DAL;
using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Services;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDbContext<AppDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
			});

			builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

			builder.Services.AddIdentity<ApplicationUsers,IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			builder.Services.AddScoped<IAuthService,AuthService>();
			builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			Console.WriteLine("JWT Secret: " + builder.Configuration["ApiSettings:JwtOptions:Secret"]);


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseAuthentication();


			app.MapControllers();
			ApplyMigration();
			app.Run();

			void ApplyMigration() 
			{
			using(var scope = app.Services.CreateScope())
				{
					var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

					if (_db.Database.GetPendingMigrations().Count() > 0)
					{
						_db.Database.Migrate();
					}
				}

			}
		}
	}
}
