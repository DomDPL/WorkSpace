using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.Admin.Sales
{
    [DataContract]
    public class SalesTotalsResponse : ResponseBase
    {
        [DataMember]
        public int OrderCount { get; set; }

        [DataMember]
        public decimal OrderTotal { get; set; }
    }
}