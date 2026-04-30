using AutoMapper;
using Domain.Models.Identity;
using Domain.Models.Outfit;
using Domain.Models.Outfit.outfits_recommendation;
using Shared.Dtos.OutfitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // From Request DTO (Mobile) to Entity (Database)
            CreateMap<CreateOutfitHistoryDto, OutfitHistory>();

            // From Entity (Database) to Response DTO (Mobile)
            //CreateMap<OutfitHistory, OutfitHistoryDto>()
            //    // Resolve full URLs for all images using your custom Resolver
            //    .ForMember(d => d.TopImagePath, o => o.MapFrom<OutfitPictureUrlResolver>())
            //    .ForMember(d => d.BottomImagePath, o => o.MapFrom<OutfitPictureUrlResolver>())
            //    .ForMember(d => d.ShoeImagePath, o => o.MapFrom<OutfitPictureUrlResolver>())
            //    .ForMember(d => d.AccessoryImagePath, o => o.MapFrom<OutfitPictureUrlResolver>())
            //    .ForMember(d => d.BagImagePath, o => o.MapFrom<OutfitPictureUrlResolver>())
            //
            //    // Map basic properties
            //    .ForMember(d => d.OriginalScore, o => o.MapFrom(s => s.OriginalScore))
            //    .ForMember(d => d.ImprovedScore, o => o.MapFrom(s => s.ImprovedScore))
            //    .ForMember(d => d.IsCompatible, o => o.MapFrom(s => s.IsCompatible))
            //
            //    // Deserialize ReplacementsJson string from DB back into a Dictionary for the Mobile app
            //    .ForMember(d => d.Replacements, o => o.MapFrom(s =>
            //        string.IsNullOrEmpty(s.ReplacementsJson)
            //        ? null
            //        : JsonSerializer.Deserialize<Dictionary<string, string>>(s.ReplacementsJson, (JsonSerializerOptions?)null)));
            
            // Outfit History -> DTO
            CreateMap<OutfitHistory, OutfitHistoryDto>()
                 .ForMember(d => d.TopImagePath,
                     o => o.MapFrom(s => s.TopImagePath))

                 .ForMember(d => d.BottomImagePath,
                     o => o.MapFrom(s => s.BottomImagePath))

                 .ForMember(d => d.ShoeImagePath,
                     o => o.MapFrom(s => s.ShoeImagePath))

                 .ForMember(d => d.AccessoryImagePath,
                     o => o.MapFrom(s => s.AccessoryImagePath))

                 .ForMember(d => d.BagImagePath,
                     o => o.MapFrom(s => s.BagImagePath))

                 .ForMember(d => d.OriginalScore,
                     o => o.MapFrom(s => s.OriginalScore))

                 .ForMember(d => d.ImprovedScore,
                     o => o.MapFrom(s => s.ImprovedScore))

                 .ForMember(d => d.IsCompatible,
                     o => o.MapFrom(s => s.IsCompatible))

                 .ForMember(d => d.Replacements,
                     o => o.MapFrom(s => DeserializeReplacements(s.ReplacementsJson)));


            // 3. User Profile Mapping
            CreateMap<AppUser, ProfileDto>()
                .ForMember(d => d.FullName, s => s.MapFrom(src => src.FullName ?? src.UserName));

            //4. Outfit Recommendations Mapping
            CreateMap<OutfitRecommendationsDto, OutfitRecommendations>().ReverseMap();

            CreateMap<UserSavedOutfit, SavedOutfitResponseDto>()
                .ForMember(dest => dest.Outfit, opt => opt.MapFrom(src => src.Outfit)); 
        }

        private static Dictionary<string, string>? DeserializeReplacements(string? json)
        {
            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<Dictionary<string, string>>(json);
        }
    }
}

        

    
