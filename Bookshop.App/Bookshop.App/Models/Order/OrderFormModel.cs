using Bookshop.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.App.Models.Order
{
    public class OrderFormModel
    {
        public int Id { get; set; }
        [Required]
        public List<OrderPositionFormModel> Positions { get; set; } = null!;

        public float Amount { get; set; }

     
    }
}
