using Finaktiva.Services;
using Finaktiva.Services.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Finaktiva.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserServiceAsync _service;

        public UsersController(IUserServiceAsync service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));

        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserModelView>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModelView), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _service.GetAsync(id);
            if (user != null)
                return Ok(user);
            return NotFound();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(int id, UserModelView user)
        {
            user.Id = id;
            var result = await _service.PutAsync(user);
            if(result)
                return NoContent();
            return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] UserModelView user)
        {
            try
            {
                var id = await _service.PostAsync(user);
                return CreatedAtAction(nameof(Get), new { id });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
