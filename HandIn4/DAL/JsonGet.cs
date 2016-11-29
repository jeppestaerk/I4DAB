using System.Net.Http;
using System.Net.Http.Headers;

namespace HandIn4.DAL
{
  class JsonGet
  {
    private readonly string _url;

    public JsonGet(string url)
    {
      _url = url;
    }

    public string GetJson()
    {
      string result = null;

      HttpClient client = new HttpClient();
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      using(HttpResponseMessage response = client.GetAsync(_url).Result)
      {
        if(response.IsSuccessStatusCode)
        {
          result = response.Content.ReadAsStringAsync().Result;
        }
      }

      return result;
    }
  }
}
