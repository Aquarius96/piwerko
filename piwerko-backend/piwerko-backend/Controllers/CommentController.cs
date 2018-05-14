using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Piwerko.Api.Interfaces;
using Piwerko.Api.Models.DB;

namespace Piwerko.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/comment")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] Comment comment)
        {
            var result = _commentService.Update(comment);
            if (result == null) return BadRequest("Blad w polaczeniu");
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Comment comment)
        {
            var result = _commentService.Add(comment);
            if (result == null) return BadRequest("Blad w polaczeniu");
            return Ok(result);
        }

        [HttpPost("get")]
        public IActionResult GetByBeerId([FromBody]JObject data) //id
        {
            int index = data["id"].ToObject<Int32>();
            var result = _commentService.GetByBeerId(index);
            if (result == null) return BadRequest("lista pusta");
            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult DeleteById([FromBody]JObject data) //id
        {
            int index = data["id"].ToObject<Int32>();
            if (_commentService.Delete(index)) return Ok("Usunieto");
            return BadRequest("Ups cos poszlo nie tak");
        }
    }
}
