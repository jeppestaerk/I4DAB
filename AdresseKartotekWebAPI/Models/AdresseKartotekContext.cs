namespace AdresseKartotekWebAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdresseKartotekContext : DbContext
    {
        public AdresseKartotekContext()
            : base("name=AdresseKartotekContext")
        {
        }

        public virtual DbSet<Adresse> Adresses { get; set; }
        public virtual DbSet<AlternativAdresse> AlternativAdresses { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Telefon> Telefons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adresse>()
                .HasMany(e => e.People)
                .WithRequired(e => e.Adresse)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Telefons)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);
        }
    }
}
