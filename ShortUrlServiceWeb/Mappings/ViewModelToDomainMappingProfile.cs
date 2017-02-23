using AutoMapper;
using ShortUrlService.Model;
using ShortUrlServiceWeb.ViewModels;

namespace ShortUrlServiceWeb.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        public ViewModelToDomainMappingProfile()
        {
            CreateMap<HistoryRecordViewModel, HistoryRecord>();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<HistoryRecordViewModel, HistoryRecord>()
            //    .ForMember(g => g.UrlLong, map => map.MapFrom(vm => vm.UrlLong))
            //    .ForMember(g => g.UrlShort, map => map.MapFrom(vm => vm.UrlShort))
            //    .ForMember(g => g.HitCount, map => map.MapFrom(vm => vm.HitCount))
            //    .ForMember(g => g.CreateDate, map => map.MapFrom(vm => vm.CreateDate));
            //});

            //var config = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<HistoryRecordViewModel, HistoryRecord>()
            //    .ForMember(g => g.UrlLong, map => map.MapFrom(vm => vm.UrlLong))
            //    .ForMember(g => g.UrlShort, map => map.MapFrom(vm => vm.UrlShort))
            //    .ForMember(g => g.HitCount, map => map.MapFrom(vm => vm.HitCount))
            //    .ForMember(g => g.CreateDate, map => map.MapFrom(vm => vm.CreateDate));
            //});

            //IMapper mapper = config.CreateMapper();
            //var source = new HistoryRecordViewModel();
            //var dest = mapper.Map<HistoryRecordViewModel, HistoryRecord>(source);
        }

        //protected override void Configure()
        //{
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.CreateMap<HistoryRecordViewModel, HistoryRecord>()
        //        .ForMember(g => g.UrlLong, map => map.MapFrom(vm => vm.UrlLong))
        //        .ForMember(g => g.UrlShort, map => map.MapFrom(vm => vm.UrlShort))
        //        .ForMember(g => g.HitCount, map => map.MapFrom(vm => vm.HitCount))
        //        .ForMember(g => g.CreateDate, map => map.MapFrom(vm => vm.CreateDate));
        //    });

        //    IMapper mapper = config.CreateMapper();
        //    var source = new HistoryRecordViewModel();
        //    var dest = mapper.Map<HistoryRecordViewModel, HistoryRecord>(source);

        //    //Mapper.CreateMap<HistoryRecordFormViewModel, HistoryRecord>()
        //    //    .ForMember(g => g.UrlLong, map => map.MapFrom(vm => vm.UrlLong))
        //    //    .ForMember(g => g.UrlShort, map => map.MapFrom(vm => vm.UrlShort))
        //    //    .ForMember(g => g.HitCount, map => map.MapFrom(vm => vm.HitCount))
        //    //    .ForMember(g => g.CreateDate, map => map.MapFrom(vm => vm.CreateDate));
        //}
    }
}