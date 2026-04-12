using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.Admin.Catalog
{
    [DataContract]
    public class AdminCatalogResponse : ResponseBase
    {
        [DataMember]
        public WebStoreCatalog Catalog { get; set; }
    }
}