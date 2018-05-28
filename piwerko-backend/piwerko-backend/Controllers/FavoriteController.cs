using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Interfaces;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IBeerService _beerService;

        public FavoriteController(IFavoriteService favoriteService, IBeerService beerService)
        {
            _favoriteService = favoriteService;
            _beerService = beerService;
        }

        [HttpPost("add")]
        public IActionResult addFavorite([FromBody]JObject data)
        {
            int _user_id = data["user_id"].ToObject<Int32>();
            int _beer_id = data["id_beer"].ToObject<Int32>();

            _favoriteService.Add(new Models.DB.FavoriteBeer { user_id = _user_id, beer_id = _beer_id });

            return Ok(_beerService.GetBeerById(_beer_id));
        }

        [HttpPost("find")]
        public IActionResult Find([FromBody] int id)
        {
            return Accepted(_favoriteService.FindFavorite(id));
        }

        [HttpPost("find")]
        public IActionResult Del([FromBody]JObject data)
        {
            int _user_id = data["user_id"].ToObject<Int32>();
            int _beer_id = data["id_beer"].ToObject<Int32>();

            _favoriteService.Delete(_user_id, _beer_id);
            return Accepted();
        }



    }
}