﻿namespace N3O.Umbraco.Payments.Bambora.Models {
    public partial class BamboraPayment {
        private void ClearErrors() {
            BamboraErrorCode = null;
            BamboraErrorMessage = null;
        }
    }
}