using MediatR;
using VideoService.Data;
using VideoService.Data.Model;
using VideoService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace VideoService.Features.Videos
{
    public class RemoveVideoCommand
    {
        public class RemoveVideoRequest : IRequest<RemoveVideoResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveVideoResponse { }

        public class RemoveVideoHandler : IAsyncRequestHandler<RemoveVideoRequest, RemoveVideoResponse>
        {
            public RemoveVideoHandler(VideoServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveVideoResponse> Handle(RemoveVideoRequest request)
            {
                var video = await _context.Videos.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                video.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveVideoResponse();
            }

            private readonly VideoServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
