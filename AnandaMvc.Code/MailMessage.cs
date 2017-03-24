using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnandaMvc.Code
{
    public class MailMessage
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
