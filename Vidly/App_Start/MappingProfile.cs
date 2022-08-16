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
            Mapper.CreateMap<Models.EntityFramework.Costumer, Dtos.CostumerDto>();
            Mapper.CreateMap<Models.EntityFramework.Movie, Dtos.MovieDto>();
            Mapper.CreateMap<Models.EntityFramework.MembershipType, Dtos.MembershipTypeDto>();
            Mapper.CreateMap<Models.EntityFramework.Genre, Dtos.GenreDto>();
            //Mapper.CreateMap<Models.EntityFramework.NewRental, Dtos.NewRentalDto>();

            Mapper.CreateMap<Dtos.CostumerDto, Models.EntityFramework.Costumer>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Dtos.MovieDto, Models.EntityFramework.Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Dtos.GenreDto, Models.EntityFramework.Genre>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<Dtos.MovieDto, Models.EntityFramework.Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            //Mapper.CreateMap<Dtos.NewRentalDto, Models.EntityFramework.NewRental>().ForMember(m => m.Id, opt => opt.Ignore());
        }
    }
}