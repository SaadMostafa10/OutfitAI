using AutoMapper;
using Domain.Models.Outfit;
using Services.Abstractions.URLService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class ImageUrlResolver : IValueResolver<OutfitHistory, object, string>
    {
        private readonly IUrlService _urlService;

        public ImageUrlResolver(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public string Resolve(
            OutfitHistory source,
            object destination,
            string destMember,
            ResolutionContext context)
        {
            return _urlService.BuildImageUrl(destMember);
        }
    }
}
