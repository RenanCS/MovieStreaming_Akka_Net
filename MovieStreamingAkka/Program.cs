using Akka.Actor;
using MovieStreamingAkka.Common.Actors;
using MovieStreamingAkka.Common.Messages;
using System;
using System.Threading;

namespace MovieStreamingAkka
{
    internal class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {


            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            // Exemplo1();

            //Exemplo2();

            Exemplo3();

            // Tell actor system (and all child actors) to shutdown
            MovieStreamingActorSystem.Terminate();

            //Wait for actor system to finish shutting down
            MovieStreamingActorSystem.WhenTerminated.Wait();

            Console.WriteLine("Actor system shutdown");

            // Press any key to stop console application
            Console.ReadKey();
        }

        private static void Exemplo3()
        {
            Console.WriteLine("Creating actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActorWithChildren>(), "Playback");


            do
            {
                ShortPause();

                Console.WriteLine();
                Console.WriteLine(("entre a command and hit enter"));

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    break;
                }

            } while (true);
        }

        private static void Exemplo2()
        {
            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending a PlayMovieMessage (Codenan the Destroyer)");
            userActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 1));

            Console.ReadKey();
            Console.WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
            userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 2));

            Console.ReadKey();
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage(1));

            Console.ReadKey();
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage(2));
        }

        private static void Exemplo1()
        {
            Props paybackActorProps = Props.Create<PlaybackActor>();

            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(paybackActorProps, "PlaybackActor");


            playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: The movie", 5));
            playbackActorRef.Tell(new PlayMovieMessage("Vingadores: O Filme", 1));
            playbackActorRef.Tell(new PlayMovieMessage("Vingadores: O Filme", 2));
            playbackActorRef.Tell(new PlayMovieMessage("Vingadores: O Filme", 3));
            playbackActorRef.Tell(new PlayMovieMessage("Vingadores: O Filme", 4));

            // PoisonPill to an actor will stop the actor when the message is processed
            playbackActorRef.Tell(PoisonPill.Instance);
        }


        private static void ShortPause()
        {
            Thread.Sleep(300);
        }
    }
}
