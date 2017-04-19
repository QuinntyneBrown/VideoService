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
    public class AddOrUpdateVideoCommand
    {
        public class AddOrUpdateVideoRequest : IRequest<AddOrUpdateVideoResponse>
        {
            public VideoApiModel Video { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateVideoResponse { }

        public class AddOrUpdateVideoHandler : IAsyncRequestHandler<AddOrUpdateVideoRequest, AddOrUpdateVideoResponse>
        {
            public AddOrUpdateVideoHandler(VideoServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateVideoResponse> Handle(AddOrUpdateVideoRequest request)
            {
                var entity = await _context.Videos
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Video.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Videos.Add(entity = new Video() { TenantId = tenant.Id });
                }

                entity.Name = request.Video.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateVideoResponse();
            }

            private readonly VideoServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
