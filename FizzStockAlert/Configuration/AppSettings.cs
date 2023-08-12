using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzStockAlert.Configuration
{
    public class AppSettings
    {
        public string SMTPHost { get; set; } = string.Empty;
        public string SMTPUser { get; set; } = string.Empty;
        public string SMTPPassword { get; set; } = string.Empty;
        public string SMTPFromEmail { get; set; } = string.Empty;
        public int SMTPPort{ get; set; }
        public string Target { get; set; } = string.Empty;
        public string[] ToEmail { get; set; } = Array.Empty<string>();
    }
}
