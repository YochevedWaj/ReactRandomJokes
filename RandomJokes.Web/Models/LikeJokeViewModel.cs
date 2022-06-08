using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomJokes.Web.Models
{
    public class LikeJokeViewModel
    {
        public int JokeID { get; set; }
        public bool Like { get; set; }
    }
}
