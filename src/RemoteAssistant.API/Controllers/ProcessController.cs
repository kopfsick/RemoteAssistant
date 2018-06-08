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

        [Route("isrunning"),HttpGet]

        public async Task<ActionResult<bool>> IsProcessRunning(string name)
        {
            return Ok(Process.GetProcessesByName(name).Any());
        }

        [Route("kill"), HttpGet]
        public async Task<ActionResult<bool>> KillProcess(string name)
        {
            var processes = Process.GetProcessesByName(name);

            foreach (var process in processes)
                process.Kill();

            return Ok(processes.Any());
        }

        [Route("start"), HttpGet]
        public async Task<ActionResult<bool>> StartProcess(string name, bool allowMultiple = false)
        {
            //TODO rename 'name' to 'path'?

            //TODO Need to check if file path exists
            //TODO What to return when file path doesn't exist?

            //TODO Can't check if process is running by using 'IsProcessRunning', it checks by name.
            //TODO Need to check by full path? 'name'/'filepath' could be relative ("calc.exe"/"calc")

            var startedNewProcess = false;

            if (allowMultiple || !(await IsProcessRunning(name)).Value)
            {
                Process.Start(name);
                startedNewProcess = true;
            }

            return Ok(startedNewProcess);
        }

        private static Process[] GetAllProcesses() => Process.GetProcesses();
    }
}