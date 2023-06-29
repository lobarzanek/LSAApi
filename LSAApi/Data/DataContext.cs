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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuration
            modelBuilder.Entity<Configuration>()
                .HasOne(e => e.Switch)
                .WithMany(e => e.Configurations)
                .HasForeignKey(e => e.SwitchId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Configuration>()
                .HasOne(e => e.ConfigStatus)
                .WithMany(e => e.Configurations)
                .HasForeignKey(e => e.ConfigStatusId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Configuration>()
                .HasOne(e => e.User)
                .WithMany(e => e.Configurations)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            //ConfigurationVlan
            modelBuilder.Entity<ConfigurationVlan>()
                .HasOne(e => e.Configuration)
                .WithMany(e => e.ConfigurationVlans)
                .HasForeignKey(e => e.ConfigurationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ConfigurationVlan>()
                .HasOne(e => e.Vlan)
                .WithMany(e => e.ConfigurationVlans)
                .HasForeignKey(e => e.VlanId)
                .OnDelete(DeleteBehavior.SetNull);

            //Model
            modelBuilder.Entity<Model>()
                 .HasOne(e => e.Producent)
                 .WithMany(e => e.Models)
                 .HasForeignKey(e => e.ProducentId)
                 .OnDelete(DeleteBehavior.SetNull);

            //Switch
            modelBuilder.Entity<Switch>()
                .HasOne(e => e.Model)
                .WithMany(e => e.Switchs)
                .HasForeignKey(e => e.ModelId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Switch>()
                .HasOne(e => e.SwitchStatus)
                .WithMany(e => e.Switches)
                .HasForeignKey(e => e.SwitchStatusId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Switch>()
                .HasOne(e => e.Section)
                .WithMany(e => e.Switchs)
                .HasForeignKey(e => e.SectionId)
                .OnDelete(DeleteBehavior.SetNull);

            //User
            modelBuilder.Entity<User>()
                .HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }


}
