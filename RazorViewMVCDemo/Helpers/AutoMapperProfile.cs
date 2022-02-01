using System;
using System.Linq;
using AutoMapper;
using RazorViewMVCDemo.Models;
using RazorViewMVCDemo.ViewModels;

namespace RazorViewMVCDemo.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UsersToDisplayViewModel>()
                .ForMember(dest => dest.FullName, opt => {
                    opt.MapFrom(n => $"{n.LastName} {n.FirstName}");
                })
                .ForMember(dest => dest.ActiveStatus, opt => {
                    opt.MapFrom(a => (a.IsActive == true ? "Active" : "Not active"));
                })
                .ForMember(dest => dest.Address, opt => {
                    opt.MapFrom(x => $"{x.Street} {x.State}, {x.Country}");
                })
                .ForMember(dest => dest.Photo, opt => {
                    opt.MapFrom(p => p.Photos.FirstOrDefault(x => x.IsMain == true));
                });

            CreateMap<RegisterViewModel, User>();
        }
    }
}
