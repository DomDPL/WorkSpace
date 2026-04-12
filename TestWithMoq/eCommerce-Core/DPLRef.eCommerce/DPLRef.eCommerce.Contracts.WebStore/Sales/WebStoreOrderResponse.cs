using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.WebStore.Sales
{
    [DataContract]
    public class WebStoreOrderResponse : ResponseBase
    {
        [DataMember]
        public WebStoreOrder Order { get; set; }
    }
}