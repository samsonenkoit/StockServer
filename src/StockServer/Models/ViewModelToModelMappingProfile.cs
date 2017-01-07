using AutoMapper;
using StockServer.BL.Model;
using StockServer.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models
{
    public class ViewModelToModelMappingProfile: Profile
    {
        [Obsolete]
        protected override void Configure()
        {
            CreateMap<PlaceViewModel, Place>().
                ForMember(dest => dest.GeoLocation, opt => opt.MapFrom(src => new Geolocation()
                {
                    Latitude = src.Latitude,
                    Longitude = src.Longitude
                }));
            CreateMap<Place, PlaceViewModel>().
                ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => (double)src.GeoLocation.Latitude)).
                ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => (double)src.GeoLocation.Longitude));
            
            CreateMap<OfferViewModel, Offer>();
            CreateMap<Offer, OfferViewModel>();
        }


    }
}
