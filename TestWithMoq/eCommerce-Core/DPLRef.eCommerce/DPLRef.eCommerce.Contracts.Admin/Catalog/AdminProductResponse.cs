using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.Admin.Catalog
{
    [DataContract]
    public class AdminProductResponse : ResponseBase
    {
        [DataMember]
        public Product Product { get; set; }
    }
}