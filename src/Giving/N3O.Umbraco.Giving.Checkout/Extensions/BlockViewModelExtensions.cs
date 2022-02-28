using N3O.Umbraco.Blocks;
using N3O.Umbraco.Giving.Checkout.Models;
using static N3O.Umbraco.Giving.Checkout.CheckoutConstants;

namespace N3O.Umbraco.Giving.Checkout.Extensions {
    public static class BlockViewModelExtensions {
        public static CheckoutAccountModel CheckoutAccount(this IBlockViewModel blockViewModel) {
            return blockViewModel.ModulesData.Get<CheckoutAccountModel>(BlockModuleKeys.CheckoutAccount);
        }
        
        public static CheckoutDonationModel CheckoutDonation(this IBlockViewModel blockViewModel) {
            return blockViewModel.ModulesData.Get<CheckoutDonationModel>(BlockModuleKeys.CheckoutDonation);
        }
        
        public static CheckoutRegularGivingModel CheckoutRegularGiving(this IBlockViewModel blockViewModel) {
            return blockViewModel.ModulesData.Get<CheckoutRegularGivingModel>(BlockModuleKeys.CheckoutRegularGiving);
        }
    }
}
