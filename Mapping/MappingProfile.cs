using AutoMapper;
using mego.Core.Models;
using mego.Core.Models.Resourses;
using mego.Models;
using mego.Models.Resources;
using System.Collections.Generic;
using System.Linq;

namespace mego.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            //Domain to Api resourse
            CreateMap(typeof(QueryResult<>), typeof(QueryResultRsource<>));

            CreateMap<Image, ImageResource>();

            CreateMap<Make, MakeResource>();

            CreateMap<Make, KeyValuePairResource>();

            CreateMap<Model, KeyValuePairResource>();

            CreateMap<Feature, KeyValuePairResource>();

            CreateMap<Order, SaveOrderResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(o =>
                new ContactResource
                {
                    Name = o.ContactName,
                    Email = o.ContactEmail,
                    Phone = o.ContactPhone
                }))
                .ForMember(or => or.Features, opt => opt.MapFrom(
                    o => o.Features.Select(or => or.FeatureId)));

            CreateMap<Order, OrderResource>()
                .ForMember(or => or.Make, opt => opt.MapFrom(o => o.Model.Make))
                .ForMember(or => or.Contact, opt => opt.MapFrom(o =>
                new ContactResource
                {
                    Name = o.ContactName,
                    Email = o.ContactEmail,
                    Phone = o.ContactPhone
                }))
               .ForMember(or => or.Features, opt =>
               opt.MapFrom(o => o.Features.Select(of => new KeyValuePairResource
               {
                   Id = of.Feature.Id,
                   Name = of.Feature.Name
               }
                   )));


            //Api Resourse to Domain
            CreateMap<OrderQueryResource, OrderQuery>();
            CreateMap<SaveOrderResource, Order>()
                .ForMember(o => o.Id, opt => opt.Ignore())
                .ForMember(o => o.ContactName, opt => opt.MapFrom(or => or.Contact.Name))
                .ForMember(o => o.ContactPhone, opt => opt.MapFrom(or => or.Contact.Phone))
                .ForMember(o => o.ContactEmail, opt => opt.MapFrom(or => or.Contact.Email))
                .ForMember(o => o.Features, opt => opt.Ignore())
                .AfterMap((or, o) =>
                {
                    //removve

                    var remF = o.Features
                .Where(f => !or.Features.Contains(f.FeatureId)).ToList();


                    foreach (var f in remF)
                    {
                        o.Features.Remove(f);
                    }

                    //add

                    var addF = or.Features.Where(id => !o.Features.Any(a => a.FeatureId == id))
                       .Select(id => new OrderFeature { FeatureId = id });

                    foreach (var f in addF)
                    {
                        o.Features.Add(f);
                    }

                });

        }
    }
}
