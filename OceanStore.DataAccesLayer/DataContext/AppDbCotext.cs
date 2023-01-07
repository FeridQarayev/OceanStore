using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OceanStore.DataAccesLayer.Models;

namespace OceanStore.DataAccesLayer.DataContext
{
    public class AppDbCotext : IdentityDbContext<User>
    {
        public AppDbCotext(DbContextOptions<AppDbCotext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        #region Products
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        #endregion
        public DbSet<Ammount> Ammounts { get; set; }
    }
    //class DataContextFactory : IDesignTimeDbContextFactory<AppDbCotext>
    //{
    //    public AppDbCotext CreateDbContext(string[] args)
    //    {
    //        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    //        var optionBuilder = new DbContextOptionsBuilder<AppDbCotext>();
    //        optionBuilder.UseSqlServer(configuration["ConnectionStrings:Context"]);
    //        return new AppDbCotext(optionBuilder.Options);
    //    }
    //}
}
