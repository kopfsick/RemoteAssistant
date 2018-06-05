using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using RemoteAssistant.API.Power;

namespace RemoteAssistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowerController : ControllerBase
    {
        private readonly IPowerCommand[] _powerCommands;

        public PowerController(IEnumerable<IPowerCommand> powerCommands)
        {
            _powerCommands = powerCommands.ToArray();
        }

        [HttpGet("{command}")]
        public async Task<ActionResult<string>> ExecuteCommand(string command)
        {
            var action = _powerCommands.FirstOrDefault(c =>
                string.Equals(command, c.Command, StringComparison.OrdinalIgnoreCase));

            return Ok(await (action?.ExecuteAsync() ?? Task.FromResult($"Did not recognize power command '{command}'!")));
        }

        [HttpGet]
        public ActionResult<IDictionary<string, string>> GetAvailableCommands(string command)
        {
            var baseUri = HttpContext.Request.GetEncodedUrl().TrimEnd('/','\\')+"/";
            var uris = _powerCommands.ToDictionary(c => c.Command, c => baseUri+c.Command);
            
            return Ok(uris);
        }
    }
}