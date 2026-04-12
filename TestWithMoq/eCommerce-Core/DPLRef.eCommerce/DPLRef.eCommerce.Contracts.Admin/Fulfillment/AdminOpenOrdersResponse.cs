using DPLRef.eCommerce.Common.Shared;
using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Contracts.Admin.Fulfillment
{
    [DataContract]
    public class AdminOpenOrdersResponse : ResponseBase
    {
        [DataMember]
        public AdminUnfulfilledOrder[] Orders;
    }
}