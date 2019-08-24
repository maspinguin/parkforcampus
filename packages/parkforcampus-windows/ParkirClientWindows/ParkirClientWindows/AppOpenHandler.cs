using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Diagnostics;

namespace ParkirClientWindows
{
    class AppOpenHandler
    {
        public static void CheckToken()
        {
            Debug.WriteLine("START");
            var client = new RestClient(Configuration.ENDPOINT);
            var request = new RestRequest("Auth/checkToken", Method.POST);

            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJub21vcl9pbmR1ayI6IjkwMDAwOSIsImlkX3R5cGUiOiI0IiwibGFzdF9sb2dpbiI6IjIwMTktMDgtMjEgMjA6MzM6MzQiLCJleHBpcmVkX2F0IjoiMjAxOS0wOC0yMiAwMjozMzozNCJ9.oUoP_ogn0ENUyHzCD3IGAZm-OfTmz8dWBWCzr5oCANI" }); // uses JsonSerializer

            request.AddHeader("Client-Service", "frontend-client");
            request.AddHeader("Auth-Key", "parkir_sttar");
            request.AddHeader("Content-Type", "application/json");

            //IRestResponse response = client.Execute(request);
            //var content = response.Content;

            IRestResponse<Model.ResponseModel> response2 = client.Execute<Model.ResponseModel>(request);
            var message = response2.Data.message;

            client.ExecuteAsync(request, response => {
                Debug.WriteLine(response.Content);
            });

            //var asyncHandle = client.ExecuteAsync<Model.ResponseModel>(request, response => {
            //    Debug.WriteLine(response.Data.message);
            //});

            // abort the request on demand
            //asyncHandle.Abort();


        }
    }
}
