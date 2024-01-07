using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class NhanKhau
    {
        public int Id { get; set; }
      
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public int Phong { get; set; }
        
        public DateTime NgaySinh { get; set; }
       
        public string Sdt { get; set; }
        public string Cccd { get; set; }
        public string QueQuan { get; set; }
        public DateTime NgayO {  get; set; }
        public NhanKhau() { }

        public NhanKhau(int id,  string ten, string gioiTinh, int phong, DateTime ngaySinh, string sdt, string cccd, string queQuan,DateTime ngayO)
        {
            Id = id;
            NgaySinh = ngaySinh;
            Ten = ten;
            Phong = phong;
            GioiTinh = gioiTinh;
            Cccd = cccd;
            Sdt = sdt;
            QueQuan = queQuan;
            NgayO = ngayO;
        }
    }
}
