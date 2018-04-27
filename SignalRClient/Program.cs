using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set connection
            var connection = new HubConnection("http://localhost:60599/");
            //Make proxy to hub based on hub name on server
            var myHub = connection.CreateHubProxy("RealTimeHub");
            //Start connection

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            //myHub.Invoke<string>("Send", "HELLO World ").ContinueWith(task => {
            //    if (task.IsFaulted)
            //    {
            //        Console.WriteLine("There was an error calling send: {0}",
            //                          task.Exception.GetBaseException());
            //    }
            //    else
            //    {
            //        Console.WriteLine(task.Result);
            //    }
            //});

            myHub.On<string>("addMessage", param => {
                Console.WriteLine(param);
            });

            myHub.Invoke<string>("ConfigSettings", "I'm doing something!!!").Wait();

            while(true)
            {
                string line = Console.ReadLine(); // Get string from user
                if (line == "exit") // Check string
                {
                    break;
                }

                if (line == "clear") // Check string
                {
                    Console.Clear();
                    Console.WriteLine("Connected");
                }


            }


            Console.Read();
            connection.Stop();
        }
    }
}
