using Akka.Actor;
using MovieStreaming.Common;
using MovieStreaming.Common.Actors;
using MovieStreaming.Common.Messages;
using System;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            string sysName = "MovieStreamingActor";

            ColorConsole.WriteLineGray($"Creating {sysName} System");
            using (MovieStreamingActorSystem = ActorSystem.Create(sysName))
            {

                ColorConsole.WriteLineGray($"Creating Actor Supervisory Heirarchy");
                var userActor = MovieStreamingActorSystem.ActorOf<PlaybackActor>("playback");

                do
                {
                    System.Threading.Thread.Sleep(100);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    ColorConsole.WriteLineGray("Enter a command and hit enter");

                    var command = Console.ReadLine();
                    if (command.StartsWith("play"))
                    {
                        int userID = int.Parse(command.Split(',')[1]);
                        string movieTitle = command.Split(',')[2];

                        var message = new PlayMovieMessage(movieTitle, userID);
                        MovieStreamingActorSystem.ActorSelection("/user/playback/usercoordinator").Tell(message);
                    }

                    if (command.StartsWith("stop"))
                    {
                        int userID = int.Parse(command.Split(',')[1]);
                        var message = new StopMovieMessage(userID);
                        MovieStreamingActorSystem.ActorSelection("/user/playback/usercoordinator").Tell(message);
                    }

                    if (command == "exit")
                    {
                        MovieStreamingActorSystem.Shutdown();
                        MovieStreamingActorSystem.AwaitTermination();
                        ColorConsole.WriteLineGray("Actor System Shutdown");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                } while (true);
            }
        }
    }
}
