using LibraryManagement.Repository.Data;
using LibraryManagement.Repository.Implements;
using LibraryManagement.Repository.Interfaces;
using LibraryManagement.Service.DTO;
using LibraryManagement.Service.Inplements;
using LibraryManagement.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //dang ky DbContext vao DI container
            builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("MyCnn")
            ));
            builder.Services.Configure<EmailSettings>(
                builder.Configuration.GetSection("EmailSettings"));
            //dang ky Repository, Service vao DI
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IMemberRepository, MemberRepository>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
            builder.Services.AddScoped<IBookEditionRepository, BookEditionRepository>();
            builder.Services.AddScoped<IBookEditionService, BookEditionService>();
            builder.Services.AddScoped<IBookCopyRepository, BookCopyRepository>();
            builder.Services.AddScoped<IBookCopyService, BookCopyService>();
            builder.Services.AddScoped<IBorrowPolicyRepository, BorrowPolicyRepository>();
            builder.Services.AddScoped<IBorrowPolicyService, BorrowPolicyService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IBorrowRequestRepository, BorrowRequestRepository>();
            builder.Services.AddScoped<IBorrowRequestService, BorrowRequestService>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<ILoanService, LoanService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
