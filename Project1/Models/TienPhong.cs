using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class TienPhong
    {
        public int Id { get; set; }
        public int NuocCu { get; set; }
        public int NuocMoi { get; set; }
        public int DienCu { get; set; }
        public int DienMoi { get; set; }
        public int TNo {  get; set; }
        public int Tong { get; set; }
        public int DaNop { get; set; }
        public TienPhong() { }
        public TienPhong(int id, int nuocCu, int nuocMoi, int dienCu, int dienMoi, int tNo)
        {
            Id = id;
            NuocCu = nuocCu;
            NuocMoi = nuocMoi;
            DienCu = dienCu; 
            DienMoi = dienMoi;
            TNo = tNo;
            Tong = 0;
            DaNop = 0;
        }
    }
}
