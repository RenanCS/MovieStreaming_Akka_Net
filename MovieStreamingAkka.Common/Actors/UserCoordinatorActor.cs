using Akka.Actor;
using MovieStreamingAkka.Common.Messages;
using System;
using System.Collections.Generic;

namespace MovieStreamingAkka.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);

                IActorRef childActorRef = _users[message.UserId];

                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);

                IActorRef childActorRef = _users[message.UserId];

                childActorRef.Tell(message);
            });

        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);

                _users.Add(userId, newChildActorRef);

                Console.WriteLine($"UserCoordinatorActor created new child UserActor for {userId} (Total User: {_users.Count}");
            }
        }

        protected override void PreStart()
        {
            Console.WriteLine("PRESTART => UserCoordinatorActor");
        }

        protected override void PostStop()
        {
            Console.WriteLine("POSTSTOP => UserCoordinatorActor");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine(" PRERESTART => UserCoordinatorActor because " + reason);

            base.PreRestart(reason, message);

        }

        protected override void PostRestart(Exception reason)
        {

            Console.WriteLine(" POSTRESTART => UserCoordinatorActor because " + reason);

            base.PostRestart(reason);
        }
    }
}
