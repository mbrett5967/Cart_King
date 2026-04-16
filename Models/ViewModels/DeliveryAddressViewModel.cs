using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cart_King.Models.ViewModels
{
    public class DeliveryAddressViewModel
    {
        //User fills out these fields 
       public string? FirstName { get; set; } 
       public string? LastName { get; set; } 
       public string? AddressLine1 { get; set; } 
       public string? City { get; set; }
       public string? Postcode { get; set; } 


       // Link to Identity
       public required string IdentityUserId { get; set; }
       
        

    }
}