using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RandomJokes.Data;
using RandomJokes.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RandomJokes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly string _connectionString;

        public JokesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("getrandomjoke")]
        public Joke GetRandomJoke()
        {
            using var client = new HttpClient();
            var json = client.GetStringAsync("https://jokesapi.lit-projects.com/jokes/programming/random").Result;
            var joke = JsonConvert.DeserializeObject<List<Joke>>(json);
            var repo = new JokesRepository(_connectionString);
            return repo.GetJoke(joke.First());

        }

        [Authorize]
        [HttpPost]
        [Route("sendfeedback")]
        public void SendFeedback(LikeJokeViewModel vm)
        {
            var accountRepo = new AccountRepository(_connectionString);
            var userID = accountRepo.GetUserId(User.Identity.Name);
            var jokesRepo = new JokesRepository(_connectionString);
            jokesRepo.SendFeedback(new UserLikedJoke
            {
                UserID = userID,
                JokeID = vm.JokeID,
                DateTime = DateTime.Now,
                Liked = vm.Like
            });
        }

        [HttpGet]
        [Route("getjokes")]
        public List<Joke> GetJokes()
        {
            var repo = new JokesRepository(_connectionString);
            return repo.GetJokes();
        }

        [HttpGet]
        [Route("getcounts")]
        public JokeCounts GetCounts(int jokeID)
        {
            var repo = new JokesRepository(_connectionString);
            return repo.GetCounts(jokeID);
        }

        [HttpGet]
        [Route("canlike")]
        public CanLikeViewModel CanLike(int jokeID)
        {
            var accountRepo = new AccountRepository(_connectionString);
            var userID = accountRepo.GetUserId(User.Identity.Name);
            var jokesRepo = new JokesRepository(_connectionString);
            var userLikedJokes = jokesRepo.GetJoke(jokeID).UserLikedJokes;
            return new CanLikeViewModel
            {
                DisableLike = userLikedJokes.Any(u => u.UserID == userID && (u.Liked || u.DateTime.AddSeconds(30) >= DateTime.Now)),
                DisableDislike = userLikedJokes.Any(u => u.UserID == userID && (!u.Liked || u.DateTime.AddSeconds(30) >= DateTime.Now))
            };
        }
    }
}
