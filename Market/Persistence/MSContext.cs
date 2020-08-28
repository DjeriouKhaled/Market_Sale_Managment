using System.Data.Entity;
using Market.Core.Domain;

namespace Market.Persistence
{
     public class MsContext : DbContext
    {
        public MsContext() : base("MsContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

       public virtual DbSet<Buy> Vents { get; set; }
       public virtual DbSet<BuyDetail> VentDetails { get; set; }
       public virtual DbSet<User> Users { get; set; }
       public virtual DbSet<Client> Clients { get; set; }
       public virtual DbSet<Setting> Settings { get; set; }
       public virtual DbSet<Remboursement> Remboursements { get; set; }
       public virtual DbSet<Emplacer> Emplacers { get; set; }
       public virtual DbSet<Product> Products { get; set; }
       public virtual DbSet<Categorie> Categories { get; set; }

      


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            //HasOptional: mark Categorie property optional in Product entity , can save product without Categorie
//            modelBuilder.Entity<BuyDetail>()
//                .HasOptional(s1 => s1.Product);

            modelBuilder.Entity<Product>()
                .HasOptional(s => s.Categorie);
//            
//            modelBuilder.Entity<Buy>()
//                .HasOptional(s => s.User);
//
//            modelBuilder.Entity<Buy>()
//                .HasOptional(s => s.Client);

//            modelBuilder.Entity<BuyDetail>()
//                .HasRequired(s => s.Vent);
            
            modelBuilder.Entity<Remboursement>()
                .HasRequired(s => s.Client);

        }
    }
}
