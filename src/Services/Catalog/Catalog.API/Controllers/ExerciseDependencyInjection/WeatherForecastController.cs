using Catalog.Domain.Entities;
using Catalog.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Catalog.API.Controllers.ExerciseDependencyInjection
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly CatalogContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, CatalogContext catalogContext)
        {
            _logger = logger;
            _context = catalogContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            return Ok(await _context.Products.ToListAsync());
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public string Get()
        //{
        //    var services = new ServiceCollection();
        //    //chỉ tạo ra 1 classA mỗi khi tạo mới 1 classB
        //    services.AddSingleton<IClassA, ClassA>();

        //    //mỗi lần tạo mới 1 classB thì sẽ đồng thời tạo mới 1 classA
        //    //services.AddTransient<IClassA, ClassA>();

        //    //chỉ tạo ra đúng duy nhất 1 class B dù có get service bao nhiêu lần
        //    //còn IclassA được inject vào classB sẽ tùy theo config ở trên
        //    services.AddSingleton<ClassB>();

        //    //mỗi lần getservice là một lần tạo mới classB
        //    var provider = _service.BuildServiceProvider();
        //    ClassB instanceB = provider.GetService<ClassB>();
        //    ClassB instanceB1 = provider.GetService<ClassB>();
        //    //Console.WriteLine(instanceB.GetHashCode());
        //    //Console.WriteLine(instanceB1.GetHashCode());
        //    return instanceB.actionB();
        //}
    }
}
