using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Cart_King.Models
{
    public class UserProfile
    {
        [Key]
    public int Id { get; set; }

    //connects profile to the login account
       [Required]
    public string? IdentityUserId { get; set; }

    [ForeignKey("IdentityUserId")]
    public IdentityUser? User { get; set; }
    

    // User Info
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? City { get; set; }
    public string? Postcode { get; set; }
    }
}