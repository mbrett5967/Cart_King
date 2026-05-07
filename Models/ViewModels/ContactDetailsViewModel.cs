using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models.ViewModels
{
    public class ContactDetailsViewModel
    {
        //User fills out these fields 
      
       public string? PhoneNumber { get; set; }
       public string? MobileNumber { get; set; }
       public string? Email { get; set; } 


       // Link to Identity
       public required string IdentityUserId { get; set; }
       
        

    }
}