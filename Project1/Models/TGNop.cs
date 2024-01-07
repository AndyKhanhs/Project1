using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class TGNop
    {
        
        public int Mon {  get; set; }
       
        public int Ye { get; set;}
        
        public TGNop(int mon, int ye)
        {
            Mon = mon;
            Ye = ye;
        }
    }
}
