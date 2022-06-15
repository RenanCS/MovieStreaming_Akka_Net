using Akka.Actor;
using MovieStreamingAkka.Common.Exceptions;
using System;

namespace MovieStreamingAkka.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if (exception is SimulatedCorruptStateException)
                    {
                        // Decide restart, entretanto, perde o histórico do filho
                        return Directive.Restart;
                    }
                    else if (exception is SimulatedTerribleMovieException)
                    {
                        // Decide retomar sem perder o histórico do filho
                        return Directive.Resume;
                    }
                    else
                    {
                        return Directive.Restart;
                    }
                });

        }
        protected override void PreStart()
        {
            Console.WriteLine("PRESTART => PlaybackStatisticsActor");
        }

        protected override void PostStop()
        {
            Console.WriteLine("POSTSTOP => PlaybackStatisticsActor");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine(" PRERESTART => PlaybackStatisticsActor because " + reason.Message);

            base.PreRestart(reason, message);

        }

        protected override void PostRestart(Exception reason)
        {

            Console.WriteLine(" POSTRESTART => PlaybackStatisticsActor because " + reason.Message);

            base.PostRestart(reason);
        }
    }


}
