using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCore.Template.DTOs;
using NetCore.Template.Services;

namespace NetCore.Template.BackOffice.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/my-entity")]
    [ApiController]
    public class MyEntityController : Controller
    {
        private readonly IMyEntityService myEntityService;
        private readonly ILogger<MyEntityController> logger;

        public MyEntityController(IMyEntityService myEntityService, ILogger<MyEntityController> logger)
        {
            this.myEntityService = myEntityService;
            this.logger = logger;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            logger.LogDebug("Testing log...");

            var list = myEntityService.GetAll();

            return Ok(list);
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            var entity = myEntityService.Get(key);

            return Ok(entity);
        }

        [HttpPut()]
        public IActionResult Update([FromBody]MyEntityDto updateUserRequest)
        {
            var updatedEntity = myEntityService.Update(updateUserRequest);

            return Ok(updatedEntity);
        }

        [HttpPost()]
        public IActionResult Create([FromBody]MyEntityDto createUserRequest)
        {
            var createdEntity = myEntityService.Create(createUserRequest);

            return Ok(createdEntity);
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            myEntityService.Delete(key);

            return Ok();
        }
    }
}