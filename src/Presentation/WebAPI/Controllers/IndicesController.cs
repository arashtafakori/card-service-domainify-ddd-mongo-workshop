using Microsoft.AspNetCore.Mvc;
using Module.Contract;
using Domainify.AspMvc;
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
            bool? isDeleted = null)
        {
            var request = GetRequest<GetIndicesList>();
            request.BookletId = bookletId;

            return await _bookletService.Process(request);
        }

        [HttpGet($"/v1/[controller]/{{id}}")]
        public async Task<ActionResult<IndexViewModel?>> Get(string id)
        {
            return await View(() => _bookletService.Process(new GetIndex(id: id)));
        }

        [HttpPost]
        public async Task<ActionResult<BookletViewModel?>> Add(AddIndex request)
        {
            return StatusCode(201, await _bookletService.Process(request));
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<BookletViewModel?>> EditIndexName(EditIndexName request)
        {
            var updatedItem = await _bookletService.Process(request);
            return Ok(updatedItem);
        }
        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await View(
                () => _bookletService.Process(new DeleteIndex(id)));
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Restore(string id)
        {
            return await View(
                () => _bookletService.Process(new RestoreIndex(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermanently(string id)
        {
            return await View(
                () => _bookletService.Process(new DeleteIndexPermanently(id)));
        }
        [HttpDelete("[action]/{bookletId}")]
        public async Task<IActionResult> EmptyTrash(string bookletId)
        {
            return await View(
                () => _bookletService.Process(
                    new EmptyIndicesTrash(bookletId: bookletId)));
        }
    }
} 