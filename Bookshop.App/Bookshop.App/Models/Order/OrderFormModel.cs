using Bookshop.Data.Model;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.App.Models.Order
{
    public class OrderFormModel
    {
      
        [Required]
        public List<OrderPositionFormModel> Positions { get; set; } = null!;

       
     
    }
}
