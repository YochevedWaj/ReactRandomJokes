using System;
using System.Text.Json.Serialization;

namespace RandomJokes.Data
{
    public class UserLikedJoke
    {
        public int UserID { get; set; }
        public int JokeID { get; set; }
        public DateTime DateTime { get; set; }
        public bool Liked { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Joke Joke { get; set; }
    }
}
