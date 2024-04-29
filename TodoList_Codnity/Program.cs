using Microsoft.EntityFrameworkCore;
using TodoList_Codnity.Data;
using TodoList_Codnity.Services;

namespace TodoList_Codnity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

             builder.Services.AddDbContext<TodoListDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")
             ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.")));

            builder.Services
                .AddControllersWithViews();
            builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));  

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}