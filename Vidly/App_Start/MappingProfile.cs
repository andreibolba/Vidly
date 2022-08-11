using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Vidly.App_Start
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Models.Costumer, Dtos.CostumerDto>();
            Mapper.CreateMap<Models.Movie, Dtos.MovieDto>();
            Mapper.CreateMap<Models.MembershipType, Dtos.MembershipTypeDto>();
            Mapper.CreateMap<Models.Genre, Dtos.GenreDto>();
            //Mapper.CreateMap<Models.NewRental, Dtos.NewRentalDto>();

            Mapper.CreateMap<Dtos.CostumerDto, Models.Costumer>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Dtos.MovieDto, Models.Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Dtos.GenreDto, Models.Genre>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Dtos.MovieDto, Models.Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            //Mapper.CreateMap<Dtos.NewRentalDto, Models.NewRental>().ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}