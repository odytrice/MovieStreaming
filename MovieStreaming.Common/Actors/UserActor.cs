using Akka.Actor;
using MovieStreaming.Common.Messages;
using System;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        private int UserID { get; }

        public UserActor(int userID)
        {
            UserID = userID;
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(m => ColorConsole.WriteLineRed("Error: Cannot start playing another movie before stopping existing one"));
            Receive<StopMovieMessage>(m => StopPlayingCurrentMovie());

            ColorConsole.WriteLineYellow($"UserActor {UserID} has now become Playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(m => StartPlayingMovie(m.MovieTitle));
            Receive<StopMovieMessage>(m => ColorConsole.WriteLineRed("Error: Cannot stop if nothing is playing"));

            ColorConsole.WriteLineYellow($"UserActor {UserID} has now become Stopped");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;
            ColorConsole.WriteLineYellow($"UserActor {UserID} is currently watching {movieTitle}");

            Context.ActorSelection("/user/playback/playbackstatistics/movieplaycounter").Tell(new IncrementPlayCountMessage(movieTitle));

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"UserActor {UserID} stopped watching {_currentlyWatching}");
            _currentlyWatching = null;

            Become(Stopped);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineYellow($"UserActor {UserID} Pre-Start");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineYellow($"UserActor {UserID} Post-Stop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineYellow($"UserActor {UserID} Pre-ReStart because {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineYellow($"UserActor {UserID} Post-Restart because {reason}");
            base.PostRestart(reason);
        }
    }
}
