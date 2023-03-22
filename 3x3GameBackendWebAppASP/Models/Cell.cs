using System.ComponentModel.DataAnnotations;

namespace _3x3GameBackendWebAppASP.Models
{
    public class Cell
    {
        [Key]
        public int Id { get; set; }
        public bool? State { get; set; }

        public Cell(int id, bool? state)
        {
            Id = id;
            State = state;
        }
    }
}
