using Akka.Actor;
using System;

namespace MovieStreamingAkka.Common.Actors
{
    public class PlaybackActorWithChildren : ReceiveActor
    {
        public PlaybackActorWithChildren()
        {
            Console.WriteLine("Creating a PlaybackActor");
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
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
            Console.WriteLine(" PRERESTART => PlaybackActor because " + reason.Message);

            base.PreRestart(reason, message);

        }

        protected override void PostRestart(Exception reason)
        {

            Console.WriteLine(" POSTRESTART => PlaybackActor because " + reason.Message);

            base.PostRestart(reason);
        }
    }
}
