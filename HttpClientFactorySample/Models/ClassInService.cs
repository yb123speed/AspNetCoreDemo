namespace HttpClientFactorySample.Models
{
    using System;
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc;
    public class ClassInService
    {
        private IHttpClientFactory _httpClientFactory;
        public ClassInService(IHttpClientFactory  httpClientFactory){
            _httpClientFactory=httpClientFactory;
        }

        public void HttpClientFactoryTest()
        {
            var httpClient = _httpClientFactory.CreateClient("CnBlogsWebSiteHttpClient");
            var response =httpClient.GetAsync("https://www.cnblogs.com/").Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
        
    }
}