using Microsoft.AspNetCore.Mvc;
using Module.Contract;
using Domainify.AspMvc;
using Domainify.Domain;
using Module.Domain.BookletAggregation;

namespace Module.Presentation.WebAPI
{
    [Route("v1/[controller]")] 
    [ApiController]
    public class IndicesController : ApiController
    {
        private readonly IBookletService _bookletService;

        public IndicesController(
            IBookletService projectService)
        {
            _bookletService = projectService;
        }

        [HttpGet($"/v1/booklets/{{bookletId}}/[controller]/")]
        public async Task<ActionResult<List<IndexViewModel>>> GetList(
            string bookletId,
            bool? isDeleted = null,
            string? searchValue = null)
        {
            var request = GetRequest<GetIndicesList>();
            request.BookletId = bookletId;
            var dd = await _bookletService.Process(request);
            return dd;
        }

        [HttpGet($"/v1/booklets/{{bookletId}}/[controller]/{{id}}")]
        public async Task<ActionResult<IndexViewModel?>> Get(string bookletId, string id)
        {
            return await View(() => _bookletService.Process(
                new GetIndex(bookletId: bookletId, id: id)));
        }

        [HttpPost]
        public async Task<ActionResult<BookletViewModel?>> Add(AddIndex request)
        {
            return StatusCode(201, await _bookletService.Process(request));
        }
    }
}