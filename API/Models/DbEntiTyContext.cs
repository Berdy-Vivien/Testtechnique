using Microsoft.EntityFrameworkCore;
namespace API.Models;

public class DbEntityContext:DbContext{
    public DbEntityContext (DbContextOptions<DbEntityContext> options)
            : base(options)
        {
        }

        public DbSet<Site> site{ get; set; }
        public DbSet<Privilege> privilege{ get; set; }
        public DbSet<User> user{ get; set; }
        
}