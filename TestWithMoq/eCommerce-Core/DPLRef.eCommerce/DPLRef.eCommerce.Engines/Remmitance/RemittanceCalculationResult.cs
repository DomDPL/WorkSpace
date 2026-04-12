using System.Runtime.Serialization;

namespace DPLRef.eCommerce.Engines.Remmitance
{
    [DataContract]
    public class RemittanceCalculationResult
    {
        [DataMember]
        public decimal FeeAmount { get; set; }

        [DataMember]
        public decimal RemittanceAmount { get; set; }
    }
}