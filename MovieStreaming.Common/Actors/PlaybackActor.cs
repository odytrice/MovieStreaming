using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;

namespace MovieStreaming.Common.Actors
{
    public class PlaybackActor : ReceiveActor
    {

        public PlaybackActor()
        {
            Context.ActorOf<UserCoordinatorActor>("usercoordinator");
            Context.ActorOf<PlaybackStatisticsActor>("playbackstatistics");
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow($"Play movie {message.MovieTitle} for User {message.UserID}");
        }
        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("Playback Actor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("Playback Actor Poststop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen($"Playback Actor restart because {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen($"PlaybackActor PostRestart because {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}
