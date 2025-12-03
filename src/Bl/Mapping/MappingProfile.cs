using Abyat.Bl.Dtos;
using Abyat.Bl.Dtos.AuditLog;
using Abyat.Bl.Dtos.User;
using Abyat.Da.Identity;
using Abyat.Domains.Models;
using AutoMapper;

namespace Abyat.Bl.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, UserDto>().ReverseMap();

        CreateMap<TbAuditLog, AuditLogDto>().ReverseMap();
        CreateMap<TbClient, ClientDto>().ReverseMap();
        CreateMap<TbCompany, CompanyDto>().ReverseMap();
        CreateMap<TbFeature, FeatureDto>().ReverseMap();
        CreateMap<TbImage, ImageDto>().ReverseMap();
        CreateMap<TbProcess, ProcessDto>().ReverseMap();
        CreateMap<TbProduct, ProductDto>().ReverseMap();
        CreateMap<TbProject, ProjectDto>().ReverseMap();
        CreateMap<TbProjectImage, ProjectImageDto>().ReverseMap();
        CreateMap<TbImageSize, ImageSizeDto>().ReverseMap();
        CreateMap<TbService, ServiceDto>().ReverseMap();
        CreateMap<TbServiceCategory, ServiceCategoryDto>().ReverseMap();
        CreateMap<TbServiceFeature, ServiceFeatureDto>().ReverseMap();
        CreateMap<TbProcessStep, ProcessStepDto>().ReverseMap();
        CreateMap<TbServiceProduct, ServiceProductDto>().ReverseMap();
        CreateMap<TbSlider, SliderDto>().ReverseMap();
        CreateMap<TbTestimonial, TestimonialDto>().ReverseMap();
        CreateMap<TbTestimonialImage, TestimonialImageDto>().ReverseMap();
        CreateMap<TbProductImage, ProductImageDto>().ReverseMap();
        CreateMap<TbServiceImage, ServiceImageDto>().ReverseMap();

    }
}
