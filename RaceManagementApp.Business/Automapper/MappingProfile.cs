using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RaceManagementApp.Business.DTOs;
using RaceManagementApp.Business.Models;

namespace RaceManagementApp.Business.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //mapping
            //CreateMap<UserMaster, UserMasterDto>();
            //CreateMap<UserMasterDto, UserMaster>();
        }
    }
}
