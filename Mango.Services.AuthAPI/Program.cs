
using Mango.Services.AuthAPI.DAL;
using Mango.Services.AuthAPI.Model;
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

			builder.Services.AddIdentity<ApplicationUsers,IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders();

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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
