using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace WebGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Post<Student>(new Student() { StudentName="Manoj", StudentId=10088});

            p.Get<Student>();

            p.Get<Student>(10088);
            Console.ReadLine();
        }

        void Post<T>(T value)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53864/api/values/");
            var postTask = client.PostAsJsonAsync<T>($"add{typeof(T).Name}", value);
            postTask.Wait();

            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                Console.WriteLine("Inserted");
            }
            else
            {
                Console.WriteLine(result.StatusCode);
            }

        }

        void Get<T>(int id=-1)
        {
            //HTTP GET
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:53864/api/values/");
            Task<HttpResponseMessage> responseTask;
            if(id == -1)
            {
                responseTask = client.GetAsync($"get{ typeof(T).Name}s");
            }
            else
            {
                responseTask = client.GetAsync($"get{ typeof(T).Name.ToLower()}s/?id={id}");
            }
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                if (id == -1)
                {


                    var readTask = result.Content.ReadAsAsync<T[]>();
                    readTask.Wait();

                    var students = readTask.Result;

                    foreach (var a in students)
                    {
                        Console.WriteLine(a);
                    }
                    Console.WriteLine("HELLLOO");
                }
                else
                {
                    var readTask = result.Content.ReadAsAsync<T>();
                    readTask.Wait();

                    var students = readTask.Result;
                    Console.WriteLine(students);
                }
            }

        }
    }
}
