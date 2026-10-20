using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoFixacao
{
    public class LogRepository
    {
        private readonly List<string> _logList = new();

        public void AdicionarLog(string log)
        {
            _logList.Add(log);
        }

        public IReadOnlyCollection<string> ObterLogs()
        {
            return _logList.AsReadOnly();
        }
    }
}
