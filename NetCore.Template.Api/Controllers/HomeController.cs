using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetCore.Template.Configuration;
using System.Security.Cryptography;

namespace NetCore.Template.BackOffice.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ConfigurationAccessor configAccessor;

        public HomeController(ConfigurationAccessor configAccessor)
        {
            this.configAccessor = configAccessor;
        }

        [HttpGet("alive")]
        public ActionResult Alive()
        {
            return Ok("Ok");
        }

        [HttpGet("version")]
        public ActionResult Version()
        {
            return Ok(configAccessor.ApiInformation);
        }

        [HttpGet("generate-certificate")]
        public ActionResult GenerateCertificate()
        {
            var provider = new RSACryptoServiceProvider(2048);

            return Ok(provider.ToXmlString(true));
        }
    }
}
