
namespace Store.Api.Controllers.v1
{
    /// <summary>
    /// The products controller.
    /// </summary>
    /// <param name="mediator"></param>
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController(IMediator mediator) : BaseController<ProductController>(mediator)
    {
    }
}
