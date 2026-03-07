using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Services.Abstractions.Outfit;
using Services.Outfit;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager(IAuthService authService , IOutfitService outfitService) : IServiceManager
    {
        public IAuthService AuthService { get; } = authService;

        public IOutfitService OutfitService { get; } = outfitService;
    }
}
