using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.Admin.Catalog
{
    [DataContract]
    public class AdminCatalogsResponse : ResponseBase
    {
        [DataMember]
        public WebStoreCatalog[] Catalogs { get; set; }
    }
}