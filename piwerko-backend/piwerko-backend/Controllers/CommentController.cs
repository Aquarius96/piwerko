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
        private readonly IUserService _userService;
        private readonly IRateService _rateService;

        public CommentController(ICommentService commentService, IUserService userService, IRateService rateService)
        {
            _commentService = commentService;
            _userService = userService;
            _rateService = rateService;
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] Comment comment)
        /*
                   public string content { get; set; }
                    public int userId { get; set; }
                    public int beerId { get; set; }
                    public int breweryId { get; set; }
                    public string DateTime { get; set; }
    */
    {
        var result = _commentService.Update(comment);
        if (result == null) return BadRequest("Blad w polaczeniu");
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] Comment comment) 
    /*
                public string content { get; set; }
                public int userId { get; set; }
                public int beerId { get; set; }
                public int breweryId { get; set; }
                public string DateTime { get; set; }
    */
        {
            var result = _commentService.Add(comment);
            if (result == null) return BadRequest("Blad w polaczeniu");
            return Ok(result);
        }

        [HttpGet("get/{beerId}")]
        public IActionResult GetByBeerId(int beerId) //id piwa
        {
            var list = new List<dynamic>();            
            var comments = _commentService.GetByBeerId(beerId);
            if (comments == null) return NotFound("lista pusta");
            foreach (var var in comments)
            {
                var user = _userService.GetUserById(var.userId);
                var json = new { Comment = var, user.avatar_URL, user.username, Rate = _rateService.Getrate(beerId, var.userId) };
                list.Add(json);
            }
            var r = list.OrderByDescending(p => p.Comment.DateTime);
            return Ok(r);
        }

        [HttpPost("delete")]
        public IActionResult DeleteById([FromBody]JObject data) //id komentarza
        {
            int index = data["id"].ToObject<Int32>();
            if (_commentService.Delete(index)) return Ok("Usunieto");
            return BadRequest("Ups cos poszlo nie tak");
        }
    }
}
