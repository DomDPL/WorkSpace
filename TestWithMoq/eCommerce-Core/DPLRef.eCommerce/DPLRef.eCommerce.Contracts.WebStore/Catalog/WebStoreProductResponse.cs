using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.WebStore.Catalog
{
    [DataContract]
    public class WebStoreProductResponse : ResponseBase
    {
        [DataMember]
        public ProductDetail Product { get; set; }
    }
}