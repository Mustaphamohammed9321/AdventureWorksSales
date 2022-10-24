using AdventureWorkSales.API.Controllers;
using AdventureWorksSales.Core;
using AdventureWorksSales.Core.Dtos.RequestDto;
using AdventureWorksSales.Core.Dtos.ResponseDto;
using AdventureWorksSales.Web.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorksSales.Web.Controllers
{
    public class HomeController : Controller
    {
        HttpClientManager _clientManager = new HttpClientManager();
        AppSettingsFactory _appSettings = new AppSettingsFactory();
        ProductController _prodController = new ProductController(); 

        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public async Task<ActionResult> Index(/*Response res*/)
        {
            //if (object.ReferenceEquals(null, res.Data) || res == null)
            //{
            //    return View();
            //}
            //else
            //{
            //var dat = new Response();
            var response = await _clientManager.GetAllTaskAsync("GetDashBoardDetails");
            var dash = new GetDashBoardItemsResponseDto
            {
                FrontBrakeTotalSales = response.DashBoardItems.FrontBrakeTotalSales,
                HighestLineTotal = response.DashBoardItems.HighestLineTotal,
                TotalOrders = response.DashBoardItems.TotalOrders,
            };

            ViewBag.totalOrders = dash.TotalOrders;
            ViewBag.highestLineTotal = dash.HighestLineTotal;
            ViewBag.brakeSalesTotal = dash.FrontBrakeTotalSales;
            return View();
            //}
        }


        [HttpGet]
        public async Task<ActionResult> Category()
        {
            ViewBag.baseUrl = _appSettings.GetFrontEndBaseUrl();
            var dat = new GenericResponse();
            var response = await _clientManager.GetAllProductCatoriesAsync("GetAllProductCategories");
            dat.Data = response.Data;
            ViewData.Model = dat;
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> EditCategory(int? ProductCategoryID = 0)
        {
            ViewBag.baseUrl = _appSettings.GetFrontEndBaseUrl();
            var dat = new ProductCategoryResponse();
            var category = await _clientManager.GetByIdTaskAsync("GetProductCategoryById?id=", ProductCategoryID);
            dat.Data = category.Data;
            ViewData.Model = category;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditCategory(ProductCategory categoryRequest)
        {
            if (ViewBag.Name == string.Empty || ViewBag.Designation == string.Empty)
            {
                return RedirectToAction("Category", "Home");
            }
            var dat = new ProductCategory();

            var prodCat = new AdventureWorkSales.API.Helper.ProductCategory()
            {
                ProductCategoryID = categoryRequest.ProductCategoryID,
            };

            var httpResponse = await _clientManager.UpdateTaskAsync(categoryRequest.ProductCategoryID, categoryRequest);

            ////https://localhost:44373/api/Application/UpdateApplicationById?applicationId=37B1C20A-6B73-4145-9628-2AE759F8C15E
            return View();
        }


        public async Task<ActionResult> AddCategory(ProductCategory category)
        {
            if(category.Name == String.Empty)
            {
                return View(category);
            }
            else
            {
                //create Category
                var res = await _clientManager.AddProductCategoryTaskAsync(category);
                ViewBag.ResponseMessage = res.ResponseMessage.ToString();
                return View(category);
            }



            //if (object.ReferenceEquals(null, category.Name))
            //{

            //    return View(category);
            //}
            //else
            //{
            //    //create Category
            //    var res = await _clientManager.AddProductCategoryTaskAsync(category);
            //    ViewBag.ResponseMessage = res.ResponseMessage.ToString();
            //    return View();
            //}
        }






    }
}