using Akka.Actor;
using MovieStreamingAkka.Common.Messages;
using System;

namespace MovieStreamingAkka.Common.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => HandleShouldPlayMovie(message));
        }

        private bool HandleShouldPlayMovie(PlayMovieMessage message)
        {
            return message.UserId < 6;
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine($"    Play Movie Message: {message.MovieTitle}  for user '{message.UserId}'");
        }

        protected override void PreStart()
        {
            Console.WriteLine("PRESTART => PlaybackActor");
        }

        protected override void PostStop()
        {
            Console.WriteLine("POSTSTOP => PlaybackActor");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine(" PRERESTART => PlaybackActor because " + reason);

            base.PreRestart(reason, message);

        }

        protected override void PostRestart(Exception reason)
        {

            Console.WriteLine(" POSTRESTART => PlaybackActor because " + reason);

            base.PostRestart(reason);
        }

    }
}
