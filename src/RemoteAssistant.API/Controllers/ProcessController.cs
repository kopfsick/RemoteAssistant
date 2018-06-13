using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            var processes = Process.GetProcesses();

            return Ok(processes.Select(p => (p.ProcessName, p.ToString())));
        }

        [Route("isrunning"),HttpGet]

        public async Task<ActionResult<bool>> IsProcessRunning(string name)
        {
            return Ok(Process.GetProcessesByName(name).Any());
        }

        [Route("kill"), HttpPost]
        public async Task<ActionResult<bool>> KillProcess(string name)
        {
            var processes = Process.GetProcessesByName(name);

            foreach (var process in processes)
                process.Kill();

            return Ok(processes.Any());
        }

        [Route("start"), HttpPost]
        public ActionResult<bool> StartProcess(string path)
        {
            //TODO What to return when file path doesn't exist?
            //TODO Support preventing starting multiple instances

            var fileExists = System.IO.File.Exists(path);
            var startedNewProcess = false;

            if (fileExists)
            {
                Process.Start(path);
                startedNewProcess = true;
            }

            return Ok(startedNewProcess);
        }
    }
}