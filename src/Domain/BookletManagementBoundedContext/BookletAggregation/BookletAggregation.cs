//using Domainify.Domain;
//using MediatR;

//namespace Module.Domain.BookletAggregation
//{
//    public class BookletAggregation : IAggregateRoot
//    {
//        private static IMediator? _mediator;
 
//        public BookletAggregation(IMediator mediator) { _mediator = mediator; }

//        public static BookletAggregation Setup(IMediator mediator)
//        {
//            return new BookletAggregation(mediator);
//        }

//        private List<Booklet> _booklets { get; set; } = new List<Booklet>();
//        public async Task CreateBooklet(Booklet booklet)
//        {
//            await _mediator!.Send(new PreventIfTheBookletHasAlreadyExisted(booklet));
//            _booklets.Add(booklet);
//        }

//        public Booklet Booklets(string id)
//        {
//            var dd = Booklet.NewInstance("");
//            dd.SetId(id);
//            return dd;
//        }
//    }
//}
