using AutoMapper;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Easeware.Remsng.Entities
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<LcdaModel, Lcda>();
            CreateMap<List<LcdaModel>, List<Lcda>>();
        }
    }
}
