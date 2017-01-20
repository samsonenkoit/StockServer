using AutoMapper;
using StockServer.DL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServer.DL
{
    public  class EntityToEntityDefaultMappingProfile: Profile
    {
        [Obsolete]
        protected override void Configure()
        {

            CreateMap<BL.Model.Place, DL.Place>()
                .ForMember(dest => dest.GeoPoint, opt => opt.MapFrom(src => GeographyHelper.PointFromGeoPoint(src.GeoLocation)))
                .ForMember(dest => dest.Offer, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetUsers, opt => opt.Ignore());
            CreateMap<DL.Place, BL.Model.Place>()
                .ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src => new BL.Model.Geolocation((double)src.GeoPoint.Latitude,
                (double)src.GeoPoint.Longitude)));

            CreateMap<BL.Model.Offer, DL.Offer>()
                .ForMember(dest => dest.OfferTransactions, opt => opt.Ignore())
                .ForMember(dest => dest.Place, opt => opt.Ignore());
            CreateMap<DL.Offer, BL.Model.Offer>()
                .ForMember(dest => dest.AvailableAmount, opt => opt.Ignore());

            CreateMap<BL.Model.OfferTransaction, DL.OfferTransactions>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => (int)src.Type))
                .ForMember(dest => dest.OfferTransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetUsers, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetUsers1, opt => opt.Ignore())
                .ForMember(dest => dest.Offer, opt => opt.Ignore())
                .ForMember(dest => dest.PointTransactions, opt => opt.Ignore())
                .ForMember(dest => dest.UserOfferDelivery, opt => opt.Ignore());

            CreateMap<BL.Model.PointTransaction, DL.PointTransactions>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => (int)src.Type))
                .ForMember(dest => dest.AspNetUsers, opt => opt.Ignore())
                .ForMember(dest => dest.AspNetUsers1, opt => opt.Ignore())
                .ForMember(dest => dest.PointTransactionType, opt => opt.Ignore())
                .ForMember(dest => dest.OfferTransactions, opt => opt.Ignore());
        }
    }
}
