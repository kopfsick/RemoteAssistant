using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace RemoteAssistant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        [HttpGet]
        public ActionResult<object[]> GetRunningProcesses()
        {
            var processes = Process.GetProcesses();

            return Ok(processes.Select(p => (p.ProcessName, p.ToString())));
        }

        [Route("isrunning"),HttpGet]
        public ActionResult<bool> IsProcessRunning(string name)
        {
            return Ok(Process.GetProcessesByName(name).Any());
        }

        [Route("kill"), HttpPost]
        public ActionResult<bool> KillProcess(string name)
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