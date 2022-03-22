﻿using N3O.Umbraco.Payments.Lookups;
using N3O.Umbraco.Payments.Models;

namespace N3O.Umbraco.Payments.Bambora.Models {
    public partial class BamboraCredential : Credential {
        public override PaymentMethod Method => BamboraConstants.PaymentMethod;
    }
}