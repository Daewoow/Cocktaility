﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Models;

public class Bar
{
    [Key]
    public int BarId { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Address { get; set; }
    
    [Required]
    public string Photo { get; set; }
    
    [Required]
    public string Menu { get; set; }
    
    [Required]
    public string Site { get; set; }
    
    public int Rating { get; set; }
    
    public string TimeOfWork { get; set; }
    
    public IEnumerable<Tag> Tags { get; internal set; } = new List<Tag>();
    
    public IEnumerable<Favorite> FavoritedByUsers { get; set; } = new List<Favorite>();
}