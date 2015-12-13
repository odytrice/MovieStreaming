using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using System;

namespace MovieStreaming.Common.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf<MoviePlayCounterActor>("movieplaycounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                if (exception is SimulatedCorruptedStateException) return Directive.Restart;
                if (exception is SimulatedTerribleMovieException) return Directive.Resume;
                return Directive.Restart;
            });
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("Playback Statistics Actor Prestart");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("Playback Statistics Actor Poststop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"Playback Statistics Actor restart because {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"PlaybackActor Statistics PostRestart because {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}
