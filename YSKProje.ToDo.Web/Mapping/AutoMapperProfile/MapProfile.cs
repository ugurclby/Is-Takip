using AutoMapper;
using YSKProje.ToDo.DTO.DTOs.AciliyetDTOs;
using YSKProje.ToDo.DTO.DTOs.AppUserDtos;
using YSKProje.ToDo.DTO.DTOs.BildirimDTOs;
using YSKProje.ToDo.DTO.DTOs.GorevDTOs;
using YSKProje.ToDo.DTO.DTOs.RaporDTOs;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web.Mapping.AutoMapperProfile
{
    public class MapProfile :Profile
    {
        public MapProfile()
        {
            #region Aciliyet Map
            CreateMap<AciliyetInsertDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetInsertDto>();
            CreateMap<AciliyetListViewDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetListViewDto>();
            CreateMap<AciliyetUpdateDto, Aciliyet>();
            CreateMap<Aciliyet, AciliyetUpdateDto>();
            #endregion

            #region AppUser Map
            CreateMap<AppUserListViewDto, AppUser>();
            CreateMap<AppUser, AppUserListViewDto>();
            CreateMap<AppUserSignInDto, AppUser>();
            CreateMap<AppUser, AppUserSignInDto>();
            CreateMap<AppUserViewDto, AppUser>();
            CreateMap<AppUser, AppUserViewDto>();
            CreateMap<AppUserViewDto, AppUser>();
            CreateMap<AppUser, AppUserViewDto>();
            #endregion

            #region Bildirim Map
            CreateMap<BildirimListViewDto, Bildirim>();
            CreateMap<Bildirim, BildirimListViewDto>();
            #endregion

            #region Görev Map
            CreateMap<GorevInsertDto, Gorev>();
            CreateMap<Gorev, GorevInsertDto>();
            CreateMap<GorevListAllViewDto, Gorev>();
            CreateMap<Gorev, GorevListAllViewDto>();
            CreateMap<GorevListViewDto, Gorev>();
            CreateMap<Gorev, GorevListViewDto>();
            CreateMap<GorevUpdateDto, Gorev>();
            CreateMap<Gorev, GorevUpdateDto>();
            #endregion

            #region Rapor Map
            CreateMap<RaporInsertViewDto, Rapor>();
            CreateMap<Rapor, RaporInsertViewDto>(); 
            #endregion
        }
    }
}
