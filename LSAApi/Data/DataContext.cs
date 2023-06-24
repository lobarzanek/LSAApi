using Microsoft.EntityFrameworkCore;
using LSAApi.Models;

namespace LSAApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<ConfigStatus> ConfigStatuses { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<ConfigurationVlan> ConfigurationVlans { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Producent> Producents { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Switch> Switches { get; set; }
        public DbSet<SwitchStatus> SwitchStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vlan> Vlans { get; set; }
    }
}
