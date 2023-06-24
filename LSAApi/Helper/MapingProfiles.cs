using AutoMapper;
using LSAApi.Dto;
using LSAApi.Models;

namespace LSAApi.Helper
{
    public class MapingProfiles : Profile
    {
        public MapingProfiles()
        {
            //Model to GetDto
            CreateMap<ConfigStatus, GetConfigStatusDto>();
            CreateMap<Configuration, GetConfigurationDto>();
            CreateMap<ConfigurationVlan, GetConfigurationVlanDto>();
            CreateMap<Model, GetModelDto>();
            CreateMap<Producent, GetProducentDto>();
            CreateMap<Role, GetRoleDto>();
            CreateMap<Section, GetSectionDto>();
            CreateMap<Switch, GetSwitchDto>();
            CreateMap<Switch, GetSwitchCredentialsDto>();
            CreateMap<SwitchStatus, GetSwitchStatusDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Vlan, GetVlanDto>();

            //CreateDto to Model
            CreateMap<CreateConfigStatusDto, ConfigStatus>();
            CreateMap<CreateConfigurationDto, Configuration>();
            CreateMap<CreateConfigurationVlanDto, ConfigurationVlan>();
            CreateMap<CreateModelDto, Model>();
            CreateMap<CreateProducentDto, Producent>();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<CreateSectionDto, Section>();
            CreateMap<CreateSwitchDto, Switch>();
            CreateMap<CreateSwitchStatusDto, SwitchStatus>();
            CreateMap<CreateUserDto, User>();
            CreateMap<CreateVlanDto, Vlan>();

        }
    }
}
