using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Project1.Models
{
    public class MyDbContext : DbContext
    {
        
        private string connectionString = @"Data Source=KHANHLINH\SQLEXPRESS;Initial Catalog=QLPhongTro;Persist Security Info=True;User ID=sa;Password=Pkl:882003;Encrypt=False;Trust Server Certificate=True";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database connection here
            optionsBuilder.UseSqlServer(connectionString);
        }
        public MyDbContext() { 
        
        }
       
        public DbSet<NhanKhau> NhanKhauDb { get; set; }
        public DbSet<PhongDto> Phong { get; set; }       
        public DbSet<TGNop> ListTGNop {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NhanKhau>().ToTable("NhanKhau");
            modelBuilder.Entity<NhanKhau>()
                .Property(x => x.NgaySinh)
                .HasColumnType("date");
            modelBuilder.Entity<NhanKhau>()
                .Property(x => x.NgayO)
                .HasColumnType("date");

            modelBuilder.Entity<TGNop>().HasKey(t => new { t.Mon, t.Ye });

        }
        
    }
        
}

