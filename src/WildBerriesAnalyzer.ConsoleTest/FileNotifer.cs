using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Interfaces;

namespace WildBerriesAnalyzer.VkDiscontBot
{
    public class FileNotifer : INotifier
    {
        private object _lock = new object();

        public FileNotifer()
        {
            if (!Directory.Exists("./Logs"))
            {
                Directory.CreateDirectory("./Logs");
            }
        }

        public void Error(string message)
        {
            lock (_lock)
            {
                using (StreamWriter streamWriter = new StreamWriter($"./Logs/Log-{DateTime.Now:dd-MM-yyyy}.txt", true))
                {
                    streamWriter.WriteLine("Error: " + message);
                }
            }
        }

        public void Ok(string message)
        {
            lock (_lock)
            {
                using (StreamWriter streamWriter = new StreamWriter($"./Logs/Log-{DateTime.Now:dd-MM-yyyy}.txt", true))
                {
                    streamWriter.WriteLine("Ok: " + message);
                }
            }
        }

        public void Warning(string message)
        {
            lock (_lock)
            {
                using (StreamWriter streamWriter = new StreamWriter($"./Logs/Log-{DateTime.Now:dd-MM-yyyy}.txt", true))
                {
                    streamWriter.WriteLine("Warning: " + message);
                }
            }
        }
    }
}
