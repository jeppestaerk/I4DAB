using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandIn4.Models;
using Newtonsoft.Json;


namespace HandIn4.DAL
{
   public class JsonDeserialize
   {
       private List<Reading> _readings;
       
       public JsonDeserialize(List<Reading> readings)
       {
           _readings = readings;
       }

       public void DeserializeJson(string json)
       {
           DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);

           DataTable dataTable = dataSet.Tables["reading"];

           foreach (var item in dataTable.Rows)
           {
               _readings.Add(JsonConvert.DeserializeObject<Reading>(item.ToString()));
           }

       }
   }
}
