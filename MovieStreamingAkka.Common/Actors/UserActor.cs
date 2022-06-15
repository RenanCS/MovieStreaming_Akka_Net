using Akka.Actor;
using MovieStreamingAkka.Common.Messages;
using System;

namespace MovieStreamingAkka.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;
        private int _id;
        public UserActor(int id)
        {
            _id = id;
            Stopped();
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(_ => Console.WriteLine($"UserActor ({_id}) Nothing is currently playing..."));

            Console.WriteLine($"UserActor ({_id}) is becoming stopped...");
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(_ => Console.WriteLine($"UserActor ({_id}) Cannot start: Already playing {_currentlyWatching}"));
            Receive<StopMovieMessage>(_ => StopPlayingMovie());

            Console.WriteLine($"UserActor ({_id}) is playing...");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            Console.WriteLine($"UserActor ({_id}) is currently watching: {title}");

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(title));

            Become(Playing);
        }

        private void StopPlayingMovie()
        {
            _currentlyWatching = null;

            Become(Stopped);
        }

        //public UserActor()
        //{
        //    Console.WriteLine("Creating a UserActor");
        //    Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => HandleShouldPlayMovie(message));
        //    Receive<StopMovieMessage>(message => HandleStopMovieMessage(message), message => HandleShouldStopMovie());
        //}

        //private bool HandleShouldPlayMovie(PlayMovieMessage message)
        //{
        //    if (_currentlyWatching != null)
        //    {
        //        Console.WriteLine("**** Error: cannot start playing another movie before stopping existing one");
        //        return false;
        //    }

        //    return message.UserId < 6;
        //}

        //private void HandlePlayMovieMessage(PlayMovieMessage message)
        //{
        //    _currentlyWatching = message.MovieTitle;
        //    Console.WriteLine($"    User is currently watching {_currentlyWatching}'");
        //}

        //private bool HandleShouldStopMovie()
        //{
        //    if (_currentlyWatching == null)
        //    {
        //        Console.WriteLine("*** Error: cannot stop if nothing is playing");
        //        return false;
        //    }

        //    return true;
        //}

        //private void HandleStopMovieMessage(StopMovieMessage message)
        //{
        //    Console.WriteLine($"    User has stopped watching {_currentlyWatching}");
        //    _currentlyWatching = null;
        //}


        protected override void PreStart()
        {
            Console.WriteLine("PRESTART => UserActor");
        }

        protected override void PostStop()
        {
            Console.WriteLine("POSTSTOP => UserActor");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine(" PRERESTART => UserActor because " + reason.Message);

            base.PreRestart(reason, message);

        }

        protected override void PostRestart(Exception reason)
        {

            Console.WriteLine(" POSTRESTART => UserActor because " + reason.Message);

            base.PostRestart(reason);
        }


    }

}
