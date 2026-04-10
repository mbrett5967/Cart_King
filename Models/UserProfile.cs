using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cart_King.Models
{
    public class UserProfile
    {
        [Key]
    public int Id { get; set; }

    //connects profile to the login account
    [Required]
    public string? IdentityUserId { get; set; } 
    

    // User Info
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? City { get; set; }
    public string? PostCode { get; set; }
    }
}