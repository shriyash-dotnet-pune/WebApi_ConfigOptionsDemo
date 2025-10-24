using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi_ConfigOptionsDemo.Models;

namespace WebApi_ConfigOptionsDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly MySettings _settingsFromOptions;

        public ConfigController(IConfiguration config, IOptions<MySettings> options)
        {
            _config = config;
            _settingsFromOptions = options.Value;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var manualSection = _config.GetSection("MySettings");
            var manualObj = new
            {
                ApplicationName = manualSection.GetValue<string>("ApplicationName"),
                Version = manualSection.GetValue<string>("Version"),
                MaxItems = manualSection.GetValue<int>("MaxItems")
            };

            var optionsObj = new
            {
                ApplicationName = _settingsFromOptions.ApplicationName,
                Version = _settingsFromOptions.Version,
                MaxItems = _settingsFromOptions.MaxItems
            };

            return Ok(new
            {
                ReadManually = manualObj,
                ReadFromOptions = optionsObj
            });
        }
    }
}
