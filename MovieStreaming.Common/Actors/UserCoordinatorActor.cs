using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;
using System.Collections.Generic;

namespace MovieStreaming.Common.Actors
{
    public class UserCoordinatorActor: ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserID);
                IActorRef childActorRef = _users[message.UserID];
                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserID);
                IActorRef childActorRef = _users[message.UserID];
                childActorRef.Tell(message);
            });
        }

        private void CreateChildUserIfNotExists(int userID)
        {
            if(!_users.ContainsKey(userID))
            {
                var props = Props.Create(() => new UserActor(userID));
                IActorRef newChildActorRef = Context.ActorOf(props, "User" + userID);
                _users.Add(userID, newChildActorRef);

                ColorConsole.WriteLineCyan($"UserCoordinatorActor created new child UserActor for {userID} (Total Users: {_users.Count})");
            }
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineCyan("UserCoordinator Actor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineCyan("UserCoordinator Actor Poststop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineCyan($"UserCoordinator Actor restart because {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineCyan($"UserCoordinator Actor PostRestart because {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}
