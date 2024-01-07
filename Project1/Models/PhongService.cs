using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project1.Models
{
    public class PhongService
    {
        public void Create( PhongDto dto)
        {
            using (var dbContext=new MyDbContext())
            {
                dbContext.Phong.Add(dto);
                dbContext.SaveChanges();
            }
        }
        public void Increase(int id)
        {
            using (var dbContext=new MyDbContext())
            {
                var room=dbContext.Phong.FirstOrDefault(x => x.Id == id);
                room.Soluong += 1;
                dbContext.Entry(room).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            using (var dbContext=new MyDbContext())
            {
                var phong=dbContext.Phong.FirstOrDefault(x=>x.Id==id);
                dbContext.Phong.Remove(phong);
                dbContext.SaveChanges();
            }
        }
        public void Decrease1(int id)
        {
            using (var dbContext = new MyDbContext())
            {
                var phong = dbContext.Phong.FirstOrDefault(x => x.Id == id);
                phong.Soluong = phong.Soluong-1;
                dbContext.Entry(phong).State = EntityState.Modified;
                if (phong.Soluong == 0)
                {
                    dbContext.Phong.Remove(phong);
                    
                }
                
                dbContext.SaveChanges();
                
            }
        }
        public void Update()
        {
           
        }
    }
}
