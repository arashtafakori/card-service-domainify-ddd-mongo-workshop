using Microsoft.AspNetCore.Mvc;
using Module.Contract;
using Domainify.AspMvc;
using Domainify.Domain;
using Module.Domain.CardAggregation;

namespace Module.Presentation.WebAPI
{
    [Route("v1/[controller]")] 
    [ApiController]
    public class CardsController : ApiController
    {
        private readonly ICardService _cardService;

        public CardsController(
            ICardService projectService)
        {
            _cardService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedViewModel<CardViewModel>>> GetList()
        {
            var request = GetRequest<GetCardsList>();
            return await _cardService.Process(request);
        }

        [HttpGet($"/v1.1/[controller]")]
        public async Task<ActionResult<PaginatedViewModel<CardViewModel>>> GetList(
            int? pageNumber = null,
            int? pageSize = null,
            bool? isDeleted = null,
            string? searchValue = null)
        {
            var request = GetRequest<GetCardsList>();
            request.Setup(
                paginationSetting: new PaginationSetting(
                    defaultPageNumber: 1, defaultPageSize: 10));

            return await _cardService.Process(request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardViewModel?>> Get(
            string id, bool? withIndices = null)
        {
            var request = GetRequest<GetCard>();
            request.SetId(id);
            return await View(() => _cardService.Process(request));
        }

        [HttpPost]
        public async Task<ActionResult<CardViewModel?>> Create(AddCard request)
        {
            return StatusCode(201, await _cardService.Process(request));
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<CardViewModel?>> EditCardTitle(EditCard request)
        {
            var updatedItem = await _cardService.Process(request);
            return Ok(updatedItem);
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return await View(
                () => _cardService.Process(new DeleteCard(id)));
        }

        [HttpPatch("[action]/{id}")]
        public async Task<IActionResult> Restore(string id)
        {
            return await View(
                () => _cardService.Process(new RestoreCard(id)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermanently(string id)
        {
            return await View(
                () => _cardService.Process(new DeleteCardPermanently(id)));
        }
        [HttpDelete("[action]/{bookletId}")]
        public async Task<IActionResult> EmptyTrash(string bookletId)
        {
            return await View(
                () => _cardService.Process(new EmptyCardsTrash(bookletId: bookletId)));
        }
    }
}