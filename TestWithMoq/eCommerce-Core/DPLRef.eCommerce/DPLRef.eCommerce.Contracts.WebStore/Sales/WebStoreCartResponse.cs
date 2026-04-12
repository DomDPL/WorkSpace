using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.WebStore.Sales
{
    [DataContract]
    public class WebStoreCartResponse : ResponseBase
    {
        [DataMember]
        public WebStoreCart Cart { get; set; }
    }
}