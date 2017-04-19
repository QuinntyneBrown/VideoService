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
    public class GetVideoByIdQuery
    {
        public class GetVideoByIdRequest : IRequest<GetVideoByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetVideoByIdResponse
        {
            public VideoApiModel Video { get; set; } 
        }

        public class GetVideoByIdHandler : IAsyncRequestHandler<GetVideoByIdRequest, GetVideoByIdResponse>
        {
            public GetVideoByIdHandler(VideoServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetVideoByIdResponse> Handle(GetVideoByIdRequest request)
            {                
                return new GetVideoByIdResponse()
                {
                    Video = VideoApiModel.FromVideo(await _context.Videos
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly VideoServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
