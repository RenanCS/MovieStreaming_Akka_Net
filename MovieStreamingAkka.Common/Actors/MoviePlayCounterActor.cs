using Akka.Actor;
using MovieStreamingAkka.Common.Exceptions;
using MovieStreamingAkka.Common.Messages;
using System;
using System.Collections.Generic;

namespace MovieStreamingAkka.Common.Actors
{
    internal class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(HandleIncrementPlayCountMessage);
        }

        private void HandleIncrementPlayCountMessage(IncrementPlayCountMessage incrementPlayCountMessage)
        {
            if (!_moviePlayCounts.ContainsKey(incrementPlayCountMessage.MovieTitle))
            {
                _moviePlayCounts.Add(incrementPlayCountMessage.MovieTitle, 1);
            }
            else
            {
                _moviePlayCounts[incrementPlayCountMessage.MovieTitle] += 1;
            }

            // Simulated bugs
            if (_moviePlayCounts[incrementPlayCountMessage.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (incrementPlayCountMessage.MovieTitle == "Recoil")
            {
                throw new SimulatedTerribleMovieException();
            }

            Console.WriteLine($"Movie {incrementPlayCountMessage.MovieTitle} has been watched: {_moviePlayCounts[incrementPlayCountMessage.MovieTitle]} times so far");
        }

        protected override void PreStart()
        {
            Console.WriteLine("PRESTART => MoviePlayCounterActor");
        }

        protected override void PostStop()
        {
            Console.WriteLine("POSTSTOP => MoviePlayCounterActor");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine(" PRERESTART => MoviePlayCounterActor because " + reason.Message);

            base.PreRestart(reason, message);

        }

        protected override void PostRestart(Exception reason)
        {

            Console.WriteLine(" POSTRESTART => MoviePlayCounterActor because " + reason.Message);

            base.PostRestart(reason);
        }
    }
}
