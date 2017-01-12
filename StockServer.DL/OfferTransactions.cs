//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StockServer.DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfferTransactions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfferTransactions()
        {
            this.UserOfferDelivery = new HashSet<UserOfferDelivery>();
        }
    
        public int Id { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public int OfferId { get; set; }
        public string BuyUserId { get; set; }
        public int Amount { get; set; }
        public Nullable<int> PointTransactionId { get; set; }
        public int TypeId { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
        public virtual Offer Offer { get; set; }
        public virtual PointTransactions PointTransactions { get; set; }
        public virtual OfferTransactionType OfferTransactionType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserOfferDelivery> UserOfferDelivery { get; set; }
    }
}
