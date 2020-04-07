using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity; //inheritance of DbContext from EntityFramework
using DBSystem.ENTITIES;

namespace DBSystem.DAL
{
    internal class Context : DbContext
    {
        public Context() : base("FSIS")
        {

        }
        public DbSet<TeamEntity> TeamEntitys { get; set; }
        public DbSet<PlayerEntity> PlayerEntitys { get; set; }
        public DbSet<GuardianEntity> GuardianEntitys { get; set; }
    }
}
