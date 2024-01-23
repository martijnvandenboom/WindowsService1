using System.ServiceProcess;
using System.Diagnostics;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            ServiceName = "MyPowerShellService";
        }

        protected override void OnStart(string[] args)
        {
            // Start the PowerShell script in the background
            StartPowerShellScript();
        }

        protected override void OnStop()
        {
            // Stop the PowerShell script if needed
        }

        private void StartPowerShellScript()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new Process { StartInfo = psi };
            process.Start();

            // Replace "C:\Path\To\Your\Script\MyScript.ps1" with the actual path to your PowerShell script
            string scriptPath = "C:\\Windows\\Temp\\MyServiceScript.ps1";
            string command = $"& '{scriptPath}'";

            process.StandardInput.WriteLine(command);
            process.StandardInput.Close();

            // Optionally, you can read the output and error streams if needed
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();
            process.Close();
        }
    }
}




