using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RemoteAssistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<object[]>> GetRunningProcesses()
        {
            var processes = GetAllProcesses();

            return Ok(processes.Select(p => (p.ProcessName, p.ToString())));
        }

        [HttpGet("{isRunning}")]
        public async Task<ActionResult<bool>> Status(string name)
        {
            return Ok(Process.GetProcessesByName(name).Any());
        }

        private static Process[] GetAllProcesses() => Process.GetProcesses();
    }
}