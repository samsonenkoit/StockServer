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
                .ForMember(dest => dest.OfferItems, opt => opt.Ignore())
                .ForMember(dest => dest.Place, opt => opt.Ignore());
            CreateMap<DL.Offer, BL.Model.Offer>();
        }
    }
}
