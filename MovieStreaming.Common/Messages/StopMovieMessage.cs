namespace MovieStreaming.Common.Messages
{
    public class StopMovieMessage
    {
        public int UserID { get; }

        public StopMovieMessage(int userID)
        {
            UserID = userID;
        }
    }
}
