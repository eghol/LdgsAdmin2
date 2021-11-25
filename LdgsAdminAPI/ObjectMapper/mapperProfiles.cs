using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LdgsAdminAPI.DTO.db;
using LdgsAdminAPI.DTO.response;
using LdgsAdminAPI.DTO.request;


namespace LdgsAdminAPI.ObjectMapper
{

        public class profUser : Profile
        {
        public profUser()
        {
            CreateMap<reqUser, dbUser>().ForMember(ut => ut.UserType, x => x.Ignore()).
            ForMember(ut => ut.Permissions, x => x.Ignore()).
            ForMember(toObj => (toObj.UserTypeId), map => map.MapFrom(fromObj => fromObj.UserTypeId));

            CreateMap<dbUser, resUser>().
                ForMember(toObj => (toObj.UserType), map => map.MapFrom(fromObj => fromObj.UserType.Name)).
                ForMember(toObj => toObj.UserTypeID, map => map.MapFrom(fromObj => fromObj.UserType.Id)).
                ForMember(toObj => toObj.Permissions, map => map.MapFrom(fromObj => fromObj.Permissions));
                //ForMember(toObj => toObj.Permissions, map => map.MapFrom(fromObj => fromObj.Permissions));

            //,toObj.UserTypeID fromObj.UserTypeId


            // CreateMap<dbUser, reqUser>().ForMember(req => obj2.UserType, map => map.MapFrom(obj1 => new reqUser { UserType = obj1.UserType.Name })); ;
            //{
            //    Code = source.CurrencyCode,
            //    Value = source.CurrencyValue.ToString("0.00")
            //}));
            // CreateMap<reqUser, dbUser>().ForMember(db => db.UserType, map => map.Ignore för att exlkuder en prop
            // All other mappings goes here
        }
    }

 
}

