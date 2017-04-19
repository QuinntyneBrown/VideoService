using MediatR;
using VideoService.Data;
using VideoService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VideoService.Features.Videos
{
    public class GetVideosQuery
    {
        public class GetVideosRequest : IRequest<GetVideosResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetVideosResponse
        {
            public ICollection<VideoApiModel> Videos { get; set; } = new HashSet<VideoApiModel>();
        }

        public class GetVideosHandler : IAsyncRequestHandler<GetVideosRequest, GetVideosResponse>
        {
            public GetVideosHandler(VideoServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetVideosResponse> Handle(GetVideosRequest request)
            {
                var videos = await _context.Videos
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetVideosResponse()
                {
                    Videos = videos.Select(x => VideoApiModel.FromVideo(x)).ToList()
                };
            }

            private readonly VideoServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
