using System;

namespace HandIn4.Models
{
  public class Reading
  {
    public int sensorId { get; set; }
    public int appartmentId { get; set; }
    public float value { get; set; }
    public DateTime timestamp { get; set; }
  }
}