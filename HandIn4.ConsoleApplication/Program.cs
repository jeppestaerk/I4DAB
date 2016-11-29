using System;
using System.Collections.Generic;
using HandIn4.DAL;
using HandIn4.Models;

namespace HandIn4.ConsoleApplication
{
  class Program
  {
    static void Main(string[] args)
    {

      List<Reading> readings = new List<Reading>();


      for(int i = 1; i < 11; i++)
      {
        var url = "http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/" + i + ".json";
        var jsonGet = new JsonGet(url);
        var jsonDeserializer = new JsonDeserialize<Reading>(readings, "reading");
        jsonDeserializer.DeserializeJson(jsonGet.GetJson());
      }

      foreach(var reading in readings)
      {
        Console.WriteLine(reading);
      }
    }
  }
}
