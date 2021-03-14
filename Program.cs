using Homwork3.Models;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace Homwork3
{
    class Program
    {
        

        static async Task Main(string[] args)
        {

                var httpClient = new HttpClient();
                var httpResponse = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/users");
                var httpClient2 = new HttpClient();
                var httpResponse2 = await httpClient2.GetAsync("https://jsonplaceholder.typicode.com/albums");
                var httpClient3 = new HttpClient();
                var httpResponse3 = await httpClient3.GetAsync("https://jsonplaceholder.typicode.com/photos");

            if ((httpResponse.StatusCode == System.Net.HttpStatusCode.OK)&&(httpResponse2.StatusCode == System.Net.HttpStatusCode.OK)&&(httpResponse3.StatusCode == System.Net.HttpStatusCode.OK))
            {
                    var contentString = await httpResponse.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<Users>>(contentString);
                    var userIdDennis = users.Where(u => u.Name == "Mrs. Dennis Schulist").Select(u => u.Id).FirstOrDefault();
                   
                    var contentString2 = await httpResponse2.Content.ReadAsStringAsync();
                    var albums = JsonConvert.DeserializeObject<List<Albums>>(contentString2);
                //result is list
                    var filteredAlbumId = albums.Where(a => a.UserId == userIdDennis).Select(a => a.Id).ToList();

                    var contentString3 = await httpResponse3.Content.ReadAsStringAsync();
                    var photos = JsonConvert.DeserializeObject<List<Photos>>(contentString3);

                    var filteredPhotos = photos.Where(p => filteredAlbumId.Contains(p.AlbumId)).Select(p => p.Url).ToList();
                  
                    foreach (string photo in filteredPhotos)
                    {
                            Console.WriteLine(photo);
                    }
            }


            
        }
    }
}
