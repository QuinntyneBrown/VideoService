using VideoService.Data;
using VideoService.Features.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VideoService.Features.Accounts
{
    public class GetProfilesQuery
    {
        public class GetProfilesRequest : IRequest<GetProfilesResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetProfilesResponse
        {
            public ICollection<ProfileApiModel> Profiles { get; set; } = new HashSet<ProfileApiModel>();
        }

        public class GetProfilesHandler : IAsyncRequestHandler<GetProfilesRequest, GetProfilesResponse>
        {
            public GetProfilesHandler(VideoServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetProfilesResponse> Handle(GetProfilesRequest request)
            {
                var profiles = await _context.Profiles
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetProfilesResponse()
                {
                    Profiles = profiles.Select(x => ProfileApiModel.FromProfile(x)).ToList()
                };
            }

            private readonly VideoServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
