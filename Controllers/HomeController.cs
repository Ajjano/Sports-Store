using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sports_Store.Models;
using Sports_Store.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sports_Store.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string category, int productPage = 1)
        {
            //return View(_repository.Products
            //    .OrderBy(p=>p.ProductID)
            //    .Skip((productPage-1)*PageSize)
            //    .Take(PageSize));

            return View(new ProductsListViewModel
            {
                Products = _repository.Products
                .Where(p=>category==null||p.Category==category)
                .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItem = category==null?_repository.Products
                    .Count():_repository.Products
                    .Where(e=>e.Category==category).Count()
                },
                CurrentCategory=category
            });

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
