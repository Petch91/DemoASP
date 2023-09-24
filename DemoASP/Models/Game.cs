using System.ComponentModel.DataAnnotations;

namespace DemoASP.Models
{
   public class Game
   {
      [ScaffoldColumn(false)]
      public int Id { get; set; }
      public string Title { get; set; }
      public DateTime DateDeSortie { get; set; }
      public string Genre { get; set; }
      public string? Resume { get; set; }
   }
}
