using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class PhongDto
    {
        public int Id { get; set; }
        public DateTime NgayCoc {  get; set; }
        public int Soluong {  get; set; }
        public int DienBD {  get; set; }
        public int NuocBD {  get; set; }
        public PhongDto() { 
        
        }
        public PhongDto(int id, DateTime ngayCoc, int soluong, int dienBD, int nuocBD)
        {
            Id = id;
            NgayCoc = ngayCoc;
            Soluong = soluong;
            DienBD = dienBD;
            NuocBD = nuocBD;
        }
    }
}
