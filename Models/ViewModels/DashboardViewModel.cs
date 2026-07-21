using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models.ViewModels
{
    public class DashboardViewModel
    {
        //Parent Viewmodel for the profile dashboard
      
        public DeliveryAddressViewModel DeliveryAddress { get; set; } =null!;
        public ContactDetailsViewModel ContactDetails { get; set; } = null!;
        public ProductDetailsViewModel ProductDetails { get; set; } = null!;

        public BasketItemViewModel BasketItems { get; set; } = null!;
        public List<WishlistViewModel> Wishlist { get; set; } = new();

      // Helper 
        public string ActiveTab { get; set; } = "delivery";
        
        
      
        

    }
}