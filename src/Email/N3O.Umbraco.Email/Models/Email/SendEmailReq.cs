﻿using N3O.Umbraco.Attributes;
using System.Collections.Generic;

namespace N3O.Umbraco.Email.Models {
    public class SendEmailReq {
        [Name("From")]
        public EmailIdentityReq From { get; set; }

        [Name("To")]
        public IEnumerable<EmailIdentityReq> To { get; set; }

        [Name("Cc")]
        public IEnumerable<EmailIdentityReq> Cc { get; set; }

        [Name("Bcc")]
        public IEnumerable<EmailIdentityReq> Bcc { get; set; }

        [Name("Subject")]
        public string Subject { get; set; }

        [Name("Body")]
        public string Body { get; set; }
        
        [Name("Model")]
        public object Model { get; set; }
    }
}