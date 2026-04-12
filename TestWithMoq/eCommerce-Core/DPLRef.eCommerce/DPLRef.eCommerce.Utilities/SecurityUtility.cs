namespace DPLRef.eCommerce.Utilities
{
    internal class SecurityUtility : UtilityBase, ISecurityUtility
    {
        public bool SellerAuthenticated() =>
            //authenticate so long as the token <> "Invalid", NULL or ""
            Context.AuthToken != "Invalid" && !string.IsNullOrEmpty(Context.AuthToken);

        public bool BackOfficeAdminAuthenticated() =>
            //authenticate so long as the token <> "Invalid", NULL or ""
            Context.AuthToken != "Invalid" && !string.IsNullOrEmpty(Context.AuthToken);
    }
}