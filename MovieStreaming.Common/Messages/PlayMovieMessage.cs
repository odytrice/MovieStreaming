namespace MovieStreaming.Common.Messages
{
    public class PlayMovieMessage
    {
        public string MovieTitle { get; }
        public int UserID { get; }

        public PlayMovieMessage(string movieTitle, int userID)
        {
            MovieTitle = movieTitle;
            UserID = userID;
        }
    }
}
