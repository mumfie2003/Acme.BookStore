using System;
using System.Collections.Generic;
using System.Text;

namespace Acme.BookStore.Test
{
    public class EmailSendingArgs
    {
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}