using MediatR;
using VideoService.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using VideoService.Data.Model;
using static VideoService.Features.DigitalAssets.Constants;
using VideoService.Features.Core;

namespace VideoService.Features.DigitalAssets
{
    public class GetDigitalAssetsQuery
    {
        public class GetDigitalAssetsRequest : IRequest<GetDigitalAssetsResponse> { }

        public class GetDigitalAssetsResponse
        {
            public ICollection<DigitalAssetApiModel> DigitalAssets { get; set; } = new HashSet<DigitalAssetApiModel>();
        }

        public class GetDigitalAssetsHandler : IAsyncRequestHandler<GetDigitalAssetsRequest, GetDigitalAssetsResponse>
        {
            public GetDigitalAssetsHandler(IVideoServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDigitalAssetsResponse> Handle(GetDigitalAssetsRequest request)
            {
                var digitalAssets = await _cache.FromCacheOrServiceAsync<List<DigitalAsset>>(() => _context.DigitalAssets.ToListAsync(), DigitalAssetCacheKeys.DigitalAssets);

                return new GetDigitalAssetsResponse()
                {
                    DigitalAssets = digitalAssets.Select(x => DigitalAssetApiModel.FromDigitalAsset(x)).ToList()
                };
            }

            private readonly IVideoServiceContext _context;
            private readonly ICache _cache;
        }
    }
}