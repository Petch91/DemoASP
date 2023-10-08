using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
   public class Game
   {
      [ScaffoldColumn(false)]
      public int Id { get; set; }
      public string Title { get; set; }
      public DateTime DateDeSortie { get; set; }
      public Genre Genres { get; set; }
      public string? Resume { get; set; }
   }
}
