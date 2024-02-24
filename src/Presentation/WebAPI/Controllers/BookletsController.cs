using Microsoft.AspNetCore.Mvc;
using Module.Contract;
using XSwift.Mvc;
using XSwift.Domain;
using Module.Domain.BookletAggregation;

namespace Module.Presentation.WebAPI
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BookletsController : XApiController
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
            var request = GetRequest<GetBookletList>();
            return await _bookletService.Process(request);
        }

        ///// <summary>
        ///// You can send a querystring like the following example,
        ///// for adjusting the offset and limit values
        ///// Example: offset=0&limit=5
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet($"/v1.1/[controller]")]
        //public async Task<ActionResult<PaginatedViewModel<BookletViewModel>>> GetList(
        //    int? pageNumber = null,
        //    int? pageSize = null)
        //{
        //    var request = GetRequest<GetBookletList>();
        //    request.Setup(
        //        paginationSetting: new PaginationSetting(
        //            defaultPageNumber: 1, defaultPageSize: 10));

        //    return await _bookletService.Process(request);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<BookletViewModel?>> Get(string id)
        {
            return await View(() => _bookletService.Process(new GetBooklet(id)));
        }

        [HttpPost]
        public async Task<ActionResult<BookletViewModel?>> Create(CreateNewBooklet request)
        {
            var createdItem = await _bookletService.Process(request);
            return CreatedAtAction(nameof(Get), new { createdItem!.Id }, createdItem);
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<BookletViewModel?>> EditBookletTitle(EditBookletTitle request)
        {
            var updatedItem = await _bookletService.Process(request);
            return Ok(updatedItem);
        }

        //[HttpPatch("[action]/{id}")]
        //public async Task<IActionResult> Archive(string id)
        //{
        //    return await View(
        //        () => _bookletService.Process(new ArchiveBooklet(id)));
        //}
        //[HttpGet("[action]/{id}")]
        //public async Task<IActionResult> CheckItemForArchiving(string id)
        //{
        //    return await View(
        //        () => _bookletService.Process(new CheckBookletForArchiving(id)));
        //}
        //[HttpPatch("[action]/{id}")]
        //public async Task<IActionResult> Restore(string id)
        //{
        //    return await View(
        //        () => _bookletService.Process(new RestoreBooklet(id)));
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await View(
                () => _bookletService.Process(new DeleteBooklet(id)));
        }
    }
}