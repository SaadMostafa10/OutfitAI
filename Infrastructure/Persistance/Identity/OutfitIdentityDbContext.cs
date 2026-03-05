using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Identity
{
    public class OutfitIdentityDbContext(DbContextOptions<OutfitIdentityDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        
    }
}
