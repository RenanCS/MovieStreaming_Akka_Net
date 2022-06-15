using Akka.Actor;
using System;

namespace MovieStreamingAkka.Remote
{
    internal class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {

            Console.WriteLine("Creating MovieStreamingActorSystem in remote process");

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.WhenTerminated.Wait();
        }
    }
}
