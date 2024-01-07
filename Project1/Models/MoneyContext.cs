using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project1.Models
{
    public class MoneyContext : DbContext
    {
        private string customTableName;

        public int Mon { get; private set; }
        public int Ye { get; private set; }

        private string connectionString = @"Data Source=KHANHLINH\SQLEXPRESS;Initial Catalog=QLPhongTro;Persist Security Info=True;User ID=sa;Password=Pkl:882003;Encrypt=False;Trust Server Certificate=True";

        public MoneyContext()
        {
            // Default constructor without custom table name
              // Set a default name or handle appropriately
        }

        public MoneyContext(int mon, int ye)
        {
            Mon = mon;
            Ye = ye;
            customTableName=$"T{Mon}_{Ye}";
        }


        public DbSet<TienPhong> TienPhongs { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public TienPhong GetTienPhong(int id)
        {
           
            return TienPhongs.FromSqlRaw($"select * from {customTableName} where Id={id}").SingleOrDefault();
        }
        public void XoaTienPhong(int id)
        {

            Database.ExecuteSqlRaw($"delete from {customTableName} where Id={id}");
        }
        public void UpdateTienPhong(TienPhongFull t)
        {

            Database.ExecuteSqlRaw($"update {customTableName} set NuocCu={t.NuocCu},NuocMoi={t.NuocMoi},DienCu={t.DienCu},DienMoi={t.DienMoi},Tong={t.Tong},TNo={t.TNo},DaNop={t.DaNop} where Id={t.Id}");
        }
        public List<TienPhong> GetListTienPhong()
        {

            return TienPhongs.FromSqlRaw($"select * from {customTableName}").ToList();
        }

        public void CreateCustomTable()
        {
            Database.ExecuteSqlRaw($"create table {customTableName}(Id int not null primary key, NuocCu int, NuocMoi int, DienCu int, DienMoi int, TNo int, Tong int, DaNop int)");
        }
        public void AddData(TienPhong t)
        {
            Database.ExecuteSqlRaw($"insert into {customTableName} values ({t.Id},{t.NuocCu},{t.NuocMoi},{t.DienCu},{t.DienMoi},{t.TNo},{t.Tong},{t.DaNop})");
        }
    }
}
