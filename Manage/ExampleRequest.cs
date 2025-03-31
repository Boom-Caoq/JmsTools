using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jmsTools.Manage
{
    public class ExampleRequest
    {

        private static readonly HttpClient client = new HttpClient();

        
        public static async Task<string> ExecuteGetRequestAsync(string requestUrl)
        {
            try
            {
                // 发起GET请求
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode(); // 确保请求成功

                // 读取响应内容
                string responseBody = await response.Content.ReadAsStringAsync();


                //var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                return "";
            }
        }

      
    }
}
