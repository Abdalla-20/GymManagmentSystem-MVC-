using AutoMapper;
using GymManagmentSystem.BLL.Services.Members.ViewModels;
using GymManagmentSystem.BLL.Services.Sessions.ViewModels;
using GymManagmentSystem.DAL.Models;

namespace GymManagmentSystem.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        MemberProfile();
        SessionProfile();
    }

    private void MemberProfile()
    {
        CreateMap<Member, MemberDetailsViewModel>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} , {src.Address.Street} , {src.Address.City}"))
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString("d/M/yyyy")));

        CreateMap<Member, MemberViewModel>()
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

        CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

        CreateMap<Member, MemberToUpdateViewModel>()
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

        CreateMap<MemberToUpdateViewModel, Member>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.Photo, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.Address.BuildingNumber = src.BuildingNumber;
            });

        CreateMap<CreateMemberViewModel, Member>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                Street = src.Street,
                BuildingNumber = src.BuildingNumber
            }))
            .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));
    }
    private void SessionProfile()
    {
        CreateMap<Session, SessionViewModel>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name));

        CreateMap<Trainer, TrainerSelectedViewModel>();
        CreateMap<Category, CategorySelectedViewModel>();
        CreateMap<CreateSessionViewModel, Session>();
    }

}