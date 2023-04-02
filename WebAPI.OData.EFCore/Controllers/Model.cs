namespace WebAPI.OData.EFCore.Controllers
{
    public class Device
    {
        public string Id { get; set; }

        public Modem? Modem { get; set; } = null;
    }

    public class Modem 
    {
        public string Name { get; set; } = string.Empty;
   
    }

    public class DeviceDto 
    {
        public string Id { get; set; }

        public ModemDto? Modem { get; set; } = null;
    }

    public class ModemDto
    {
        public string Name { get; set; } = string.Empty;

    }


    public class Mappings : AutoMapper.Profile 
    {
        public Mappings() 
        { 
            CreateMap<Device, DeviceDto>()
                .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<Device, DeviceDto>()
                .ReverseMap()
                    .ForAllMembers(o => o.ExplicitExpansion());

            CreateMap<Modem, ModemDto>()
                .ForAllMembers(o => o.ExplicitExpansion());

        }
    
    }
}
