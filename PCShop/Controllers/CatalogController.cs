using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PCShop.Models;
using PCShop.Models.Mapping;
using PCShop.Services;

namespace PCShop.Controllers
{
    public class CatalogController : Controller
    {
        #region properties

        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _catalogService;

        #endregion

        #region ctor

        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        #endregion


        #region Methods

        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            try
            {
                var products = await _catalogService.GetAllProductsAsync(token).ConfigureAwait(false);
                return View(products.ToViewModels());
            }
            catch (Exception e)
            {
                _logger.LogError("Can't get products", e);
                return RedirectToAction(nameof(Error));
            }
        }

        public async Task<IActionResult> Product(int id, CancellationToken token = default)
        {
            try
            {
                var product = await _catalogService.GetProductById(id, token).ConfigureAwait(false);
                
                var model = new OrderViewModel{Product = product.ToViewModel()};
                
                return View(model);

            }
            catch (Exception e)
            {
                _logger.LogError($"Can't get product with id {id}", e);
                return RedirectToAction(nameof(Error));
            }
        }


        [HttpPost]
        public IActionResult Order(OrderViewModel model)
        {
            if (!ModelState.IsValid) return View("Product",  model);

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
