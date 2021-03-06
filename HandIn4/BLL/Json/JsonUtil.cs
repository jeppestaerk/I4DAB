﻿using System.Collections.Generic;
using HandIn4.DAL;
using HandIn4.Models;

namespace HandIn4.BLL.Json
{
  public class JsonUtil
  {

    private string _portnumber, _hostname, _servicepath;
    public string _fullservicepath { get; set; }


    public JsonUtil(string hostname, string portnumber, string servicepath)
    {
      _portnumber = portnumber;
      _hostname = "http://" + hostname + ":" + portnumber + "/";
      _servicepath = servicepath + "/";
      _fullservicepath = "http://" + hostname + ":" + portnumber + "/" + _servicepath;
    }

      


      public List<ReadingRootobject> getAllReadings() //Use Case 7
    {
      APIGetJSON<List<ReadingRootobject>> getevents = new APIGetJSON<List<ReadingRootobject>>(_fullservicepath);
      return getevents.data;
    }
  }
}