using System.Collections.Generic;

namespace RandomJokes.Data
{
    public class Joke
    {
        public int ID { get; set; }
        public int SourceID { get; set; }
        public string Setup { get; set; }
        public string Punchline { get; set; }
        public List<UserLikedJoke> UserLikedJokes { get; set; }
    }
}
