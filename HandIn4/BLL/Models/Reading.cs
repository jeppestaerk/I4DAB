using System;
using System.Collections.Generic;

namespace HandIn4.Models
{
    public class Reading
    {
        public int sensorId { get; set; }
        public int appartmentId { get; set; }
        public float value { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class ReadingRootobject
    {
        public int version { get; set; }
        public DateTime timestamp { get; set; }
        public List<Reading2> reading { get; set; }
    }

    public class Reading2
    {
        public int sensorId { get; set; }
        public int appartmentId { get; set; }
        public float value { get; set; }
        public DateTime timestamp { get; set; }
    }

}