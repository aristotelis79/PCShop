using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PCShop.Data;
using PCShop.Models;
using PCShop.Services;

namespace PCShop.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogController(ILogger<CatalogController> logger, ICatalogService catalogService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            try
            {
                var products = await _catalogService.GetAllProductsAsync(token).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError("Can't get products", e);
                return RedirectToAction(nameof(Error));
            }
            return View();
        }

        public async Task<IActionResult>  Product(int id, CancellationToken token = default)
        {
            try
            {
                //var _dbContext = new PcShopContext("Server=.;Database=PcShop;Trusted_Connection=True;MultipleActiveResultSets=true");
                //var d =  _dbContext.Product.Include(x=>x.ProductComponent).FirstOrDefault(x => x.Id == id);

                //_dbContext.Entry(d.ProductComponent).Collection(x => x.ChildrenProductComponents).Load();

                var product = await _catalogService.GetProductById(id, token).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                _logger.LogError($"Can't get product with id {id}", e);
                return RedirectToAction(nameof(Error));
            }

            return NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
