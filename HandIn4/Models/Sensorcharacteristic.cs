using System;

namespace HandIn4.Models
{
  public class Sensorcharacteristic
  {
    public int sensorId { get; set; }
    public string description { get; set; }
    public string unit { get; set; }
    public string externalRef { get; set; }
    public string calibrationEquation { get; set; }
    public string calibrationCoeff { get; set; }
    public DateTime calibrationDate { get; set; }
  }
}