using Microsoft.AspNetCore.Mvc;
using Module.Contract;
using Domainify.AspMvc;
using Domainify.Domain;
using Module.Domain.BookletAggregation;

namespace Module.Presentation.WebAPI
{
    [Route("v1/[controller]")] 
    [ApiController]
    public class BookletsController : ApiController
    {
        private readonly IBookletService _bookletService;

        public BookletsController(
            IBookletService projectService)
        {
            _bookletService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedViewModel<BookletViewModel>>> GetList()
        {
            var request = GetRequest<GetBookletsList>();
            return await _bookletService.Process(request);
        }

        [HttpGet($"/v1.1/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<BookletViewModel>>> GetList(
            int? pageNumber = null,
            int? pageSize = null,
            bool? isDeleted = null,
            string? searchValue = null)
        {
            var request = GetRequest<GetBookletsList>();
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _bookletService.Process(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookletViewModel?>> Get(
            string id, bool? withIndices = null)
        {
            var request = GetRequest<GetBooklet>();
            request.SetId(id);
            return await View(() => _bookletService.Process(request));
        }

        [HttpPost]
        public async Task<ActionResult<BookletViewModel?>> Create(CreateBooklet request)
        {
            return StatusCode(201, await _bookletService.Process(request));
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<BookletViewModel?>> EditBookletTitle(EditBookletTitle request)
        {
            var updatedItem = await _bookletService.Process(request);
            return Ok(updatedItem);
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await View(
                () => _bookletService.Process(new DeleteBooklet(id)));
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Restore(string id)
        {
            return await View(
                () => _bookletService.Process(new RestoreBooklet(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermanently(string id)
        {
            return await View(
                () => _bookletService.Process(new DeleteBookletPermanently(id)));
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> EmptyTrash()
        {
            return await View(
                () => _bookletService.Process(new EmptyBookletsTrash()));
        }
    }
}