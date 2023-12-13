using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildBerriesAnalyzer.Domain.Interfaces;

namespace WildBerriesAnalyzer.Console
{
    public class ConsoleNotifier : INotifier
    {
        public void Error(string message)
        {
            
        }

        public void Ok(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
