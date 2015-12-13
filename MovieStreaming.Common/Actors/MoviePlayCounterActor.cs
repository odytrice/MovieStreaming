using Akka.Actor;
using MovieStreaming.Common.Exceptions;
using MovieStreaming.Common.Messages;
using System;
using System.Collections.Generic;

namespace MovieStreaming.Common.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;
        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(m => HandleIncrementMessage(m));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            //Simulated Bugs
            if(_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptedStateException();
            }
            if (message.MovieTitle == "Partial Recoil")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteMagenta($"MoviePlayCountActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
        }

        #region Lifecycle Hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineWhite("MoviePlayCounterActor Pre-Start");
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineWhite("MoviePlayCounterActor Post-Stop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineWhite($"MoviePlayCounterActor Pre-Restart because {reason}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineWhite($"MoviePlayCounterActor Post-Restart because {reason}");
            base.PostRestart(reason);
        }
        #endregion
    }
}