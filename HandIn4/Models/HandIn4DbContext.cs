using System.Data.Entity;

namespace HandIn4.Models
{
  class HandIn4DbContext : DbContext
  {
    public DbSet<Reading> Readings { get; set; }
    public DbSet<Appartmentcharacteristic> Appartmentcharacteristics { get; set; }
    public DbSet<Sensorcharacteristic> Sensorcharacteristics { get; set; }

  }
}
