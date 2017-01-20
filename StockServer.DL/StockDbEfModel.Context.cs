﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StockDbEntities : DbContext
    {
        public StockDbEntities()
            : base("name=StockDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<OfferTransactionType> OfferTransactionType { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<PointTransactions> PointTransactions { get; set; }
        public virtual DbSet<PointTransactionType> PointTransactionType { get; set; }
        public virtual DbSet<OfferTransactions> OfferTransactions { get; set; }
        public virtual DbSet<UserOfferDelivery> UserOfferDelivery { get; set; }
        public virtual DbSet<UserActivity> UserActivity { get; set; }
        public virtual DbSet<UserActivityType> UserActivityType { get; set; }
    
        public virtual int BuyOfferProcedure(string createUserId, string buyUserId, Nullable<int> offerId, Nullable<System.DateTime> createDate, Nullable<int> amount)
        {
            var createUserIdParameter = createUserId != null ?
                new ObjectParameter("createUserId", createUserId) :
                new ObjectParameter("createUserId", typeof(string));
    
            var buyUserIdParameter = buyUserId != null ?
                new ObjectParameter("buyUserId", buyUserId) :
                new ObjectParameter("buyUserId", typeof(string));
    
            var offerIdParameter = offerId.HasValue ?
                new ObjectParameter("offerId", offerId) :
                new ObjectParameter("offerId", typeof(int));
    
            var createDateParameter = createDate.HasValue ?
                new ObjectParameter("createDate", createDate) :
                new ObjectParameter("createDate", typeof(System.DateTime));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("amount", amount) :
                new ObjectParameter("amount", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BuyOfferProcedure", createUserIdParameter, buyUserIdParameter, offerIdParameter, createDateParameter, amountParameter);
        }
    
        public virtual int EnrollmentPointsForActivityIfNeed(string targetUserId)
        {
            var targetUserIdParameter = targetUserId != null ?
                new ObjectParameter("targetUserId", targetUserId) :
                new ObjectParameter("targetUserId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EnrollmentPointsForActivityIfNeed", targetUserIdParameter);
        }
    }
}
