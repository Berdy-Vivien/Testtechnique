
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Privilege {
    [Key]
    public int Id{set;get;}
    public string? Name{set;get;}
}