using AutoMapper;
using ShortUrlService.Model;
using ShortUrlServiceWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortUrlServiceWeb.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<HistoryRecord, HistoryRecordViewModel>();
           
        }

    }
}