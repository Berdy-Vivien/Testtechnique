
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Site {
    [Key]
    public int Id{set;get;}
    public string? Name{set;get;}
    public string? Surface{set;get;}
    public string? Adress{set;get;}
}