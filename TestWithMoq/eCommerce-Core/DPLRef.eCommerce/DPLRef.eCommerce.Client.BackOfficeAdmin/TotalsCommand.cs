using DPLRef.eCommerce.Common.Contracts;
using DPLRef.eCommerce.Common.Shared;
using DPLRef.eCommerce.Contracts.BackOfficeAdmin.Remittance;
using DPLRef.eCommerce.Managers;
using System;

namespace DPLRef.eCommerce.Client.BackOfficeAdmin
{
    internal class TotalsCommand : BaseUICommand
    {
        public TotalsCommand(AmbientContext ambientContext)
            : base(ambientContext)
        {
        }

        public override string Name => "Sales Totals";

        protected override UICommandParameter[] Parameters { get; set; } = Array.Empty<UICommandParameter>();

        protected override void CallManager()
        {
            var managerFactory = new ManagerFactory(Context);
            var remittanceManager = managerFactory.CreateManager<IBackOfficeRemittanceManager>();

            var response = remittanceManager.Totals();

            if (response.Success)
            {
                ShowResponse(response, StringUtilities.DataContractToJson(response.SellerOrderData));
            }
            else
            {
                ShowResponse(response, response.Message);
            }
        }
    }
}