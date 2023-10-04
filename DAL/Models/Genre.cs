using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
   public class Genre
   {
      [ScaffoldColumn(false)]
      public int Id { get; set; }
      public string Label { get; set; }
   }
}
