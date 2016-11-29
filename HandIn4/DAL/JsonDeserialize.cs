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
   public class JsonDeserialize<T>
   {
       private List<T> _data;
       private readonly string _table;

       public JsonDeserialize(List<T> data,string table)
       {
           _data = data;
           _table = table;
       }

       public void DeserializeJson(string json)
       {
           DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);

           DataTable dataTable = dataSet.Tables[_table];

           foreach (var item in dataTable.Rows)
           {
               _data.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
           }

       }
   }
}
