using MediatR;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VideoService.Features.Core;
using static VideoService.Features.Videos.AddOrUpdateVideoCommand;
using static VideoService.Features.Videos.GetVideosQuery;
using static VideoService.Features.Videos.GetVideoByIdQuery;
using static VideoService.Features.Videos.RemoveVideoCommand;

namespace VideoService.Features.Videos
{
    [Authorize]
    [RoutePrefix("api/video")]
    public class VideoController : ApiController
    {
        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdateVideoResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdateVideoRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdateVideoResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdateVideoRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetVideosResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetVideosRequest();
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetVideoByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetVideoByIdRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemoveVideoResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemoveVideoRequest request)
        {
            request.TenantUniqueId = Request.GetTenantUniqueId();
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
    }
}
