using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        
        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected string? GetIpAddress()
        {
            //burdaki Request ControllerBase den gelir.
            //Request in Headerlarından "X-Forwarded-For" etiketini içeriyorsa bunu dönder yoksa HttpContext de mapleme yaparak IpAddress i oluşturup döner

            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }

        //İlkYOLDU
        //_mediator değişkeni başlangıçta null ise, HttpContext.RequestServices.GetService<IMediator>() ifadesini çağırarak bir IMediator örneği alır ve _mediator değişkenine atar. Bu sayede _mediator değişkeni bir kere doldurulduktan sonra tekrar null kontrolü yapılmadan her çağırıldığında aynı örneği döndürür.
        //_mediator değişkeni nullable (IMediator?) olarak tanımlandığı için, null değerlerle de çalışabilir.
        
        // public IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        // protected IMediator? _mediator;
        
        // public IMediator GetMediator()
        // {
        //     return _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        // }
    }
}
