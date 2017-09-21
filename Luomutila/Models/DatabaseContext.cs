using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LuomuTila.Models
{
    public class DatabaseContext : KoskenkorvanLuomutilaEntities3 {

        public DbSet<User> UserAccount { get; set; }
    }
}