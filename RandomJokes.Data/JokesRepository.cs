using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomJokes.Data
{
    public class JokesRepository
    {
        private readonly string _connectionString;

        public JokesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Joke> GetJokes()
        {
            using var ctx = new JokesDataContext(_connectionString);
            return ctx.Jokes.Include(j => j.UserLikedJokes).ToList();
        }
        public Joke GetJoke(Joke joke)
        {
            using var ctx = new JokesDataContext(_connectionString);
            var j = GetByID(joke.ID);
            if (j != null)
            {
                return j;

            }
            joke = AddJoke(joke);
            return joke;
        }

        private Joke AddJoke(Joke joke)
        {
            using var ctx = new JokesDataContext(_connectionString);
            var jk = new Joke { Setup = joke.Setup, Punchline = joke.Punchline, SourceID = joke.ID };
            ctx.Jokes.Add(jk);
            ctx.SaveChanges();
            return ctx.Jokes.Include(j => j.UserLikedJokes).First(j => j.SourceID == jk.SourceID);
        }

        public void SendFeedback(UserLikedJoke userLikedJoke)
        {
            using var ctx = new JokesDataContext(_connectionString);
            ctx.Database.ExecuteSqlInterpolated($"DELETE FROM UserLikedJokes WHERE UserID = {userLikedJoke.UserID}");
            ctx.UserLikedJokes.Add(userLikedJoke);
            ctx.SaveChanges();
        }

        public Joke GetByID(int id)
        {
            using var ctx = new JokesDataContext(_connectionString);
            return ctx.Jokes.Include(j => j.UserLikedJokes).FirstOrDefault(j => j.SourceID == id);
        }

        public JokeCounts GetCounts(int jokeID)
        {
            using var ctx = new JokesDataContext(_connectionString);
            return new JokeCounts
            {
                Likes = ctx.UserLikedJokes.Where(u => u.JokeID == jokeID && u.Liked).Count(),
                Dislikes = ctx.UserLikedJokes.Where(u => u.JokeID == jokeID && !u.Liked).Count()
            };
        }
    }
}
