using Akka.Actor;
using MovieStreaming.Common;

namespace MovieStreaming.Remote
{
    public class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating MovieStreamingActorSystem in Remote Process");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.AwaitTermination();
        }
    }
}
