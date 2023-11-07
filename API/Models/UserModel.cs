
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class User {
    [Key]
    public int Id{set;get;}
    public string? Name{set;get;}
    public string? Password{set;get;}

    [ForeignKey("PrivilegeId")]
    public int Privilege { get; set; }

    [ForeignKey("SiteId")]
    public int Site { get; set; }
}