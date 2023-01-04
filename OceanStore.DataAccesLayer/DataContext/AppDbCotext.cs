﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OceanStore.DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
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
