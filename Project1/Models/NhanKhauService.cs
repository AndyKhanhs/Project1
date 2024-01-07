using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class NhanKhauService
    {

        public List<NhanKhau> GetAll()
        {
            using (var dbContext=new MyDbContext())
            {
                return dbContext.NhanKhauDb.ToList();
            }
            
        }
        public List<NhanKhau> GetFrom(int id)
        {
            using (var dbContext = new MyDbContext())
            {
                return dbContext.NhanKhauDb.Where(x=>x.Phong==id).ToList();
            }
        }
        
        public void Delete(int id)
        {
            using (var dbContext = new MyDbContext() )
            {
                var person = dbContext.NhanKhauDb.FirstOrDefault(x => x.Id == id);
                dbContext.NhanKhauDb.Remove(person);
                dbContext.SaveChanges();
                
            }
            
        }
        public void DeleteFrom(int idPhong)
        {
            using (var dbContext = new MyDbContext())
            {
                List<NhanKhau> person = dbContext.NhanKhauDb.Where(x => x.Phong == idPhong).ToList();
                foreach(NhanKhau n in person)
                {
                    dbContext.NhanKhauDb.Remove(n);
                }
                
                dbContext.SaveChanges();

            }

        }
        public void Create(NhanKhau dto)
        {
            using (var dbContext = new MyDbContext())
            {

                dbContext.NhanKhauDb.Add(dto);
                dbContext.SaveChanges();
                
            }
        }
        public void Update(NhanKhau dto)
        {
            using (var dbContext = new MyDbContext())
            {
                var person=dbContext.NhanKhauDb.FirstOrDefault(x=>x.Id == dto.Id);
                person.QueQuan=dto.QueQuan;
                person.NgaySinh = dto.NgaySinh;
                person.NgayO=dto.NgayO;
                person.Cccd=dto.Cccd;
                person.GioiTinh=dto.GioiTinh;
                person.Ten = dto.Ten;
                person.Sdt=dto.Sdt;
                person.Phong = dto.Phong;
                dbContext.Entry(person).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
