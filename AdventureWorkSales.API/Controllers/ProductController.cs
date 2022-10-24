using AdventureWorkSales.API.Helper;
using AdventureWorksSales.Core;
using AdventureWorksSales.Core.Dtos.RequestDto;
using AdventureWorksSales.Core.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AdventureWorkSales.API.Controllers
{
    [RoutePrefix("api")]
    public class ProductController : ApiController
    {
        [HttpGet, AllowAnonymous, Route("GetAllProduct")]
        public async Task<IHttpActionResult> GetAllProduct()
        {
            try
            {
                using (var _context = new AdventureWorksSalesEntities())
                {
                    var products = await _context.Products.ToListAsync();
                    return Ok(new Response { Data = products, ResponseCode = "00", ResponseMessage = "Command(s) Completed Successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured, reason: {ex.Message}");
            }
        }
        
        
        [HttpGet, AllowAnonymous, Route("GetProductById")]
        public async Task<IHttpActionResult> GetProductById(int id)
        {
            try
            {
                using (var _context = new AdventureWorksSalesEntities())
                {
                    var products = await _context.Products.Where(w => w.ProductID == id).FirstOrDefaultAsync();
                    return Ok(new Response { Data = products, ResponseCode = "00", ResponseMessage = "Command(s) Completed Successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured, reason: {ex.Message}");
            }
        }


        [HttpGet, Route("GetAllProductCategories"), AllowAnonymous]
        public async Task<IHttpActionResult> GetAllProductCategories()
        {
            try
            {
                using (var _context = new AdventureWorksSalesEntities())
                {
                    var res = await _context.ProductCategories.ToListAsync();

                    return Ok(new Response 
                    { 
                        Data = res, 
                        ResponseCode = "00", 
                        ResponseMessage = "Command(s) Completed Successfullly" 
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured, reason: {ex.Message}");
            }
        }
        

        [HttpGet, Route("GetProductCategoryById"), AllowAnonymous]
        public async Task<IHttpActionResult> GetProductCategoryById(int id)
        {
            try
            {
                using (var _context = new AdventureWorksSalesEntities())
                {
                    var res = await _context.ProductCategories.Where(g => g.ProductCategoryID == id).FirstOrDefaultAsync();
                    if(res == null)
                        return Ok(new Response { Data = null, ResponseCode = "90", ResponseMessage = "No record found" });
                    return Ok(new Response { Data = res, ResponseCode = "00", ResponseMessage = "Command(s) Completed Successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured, reason: {ex.Message}");
            }
        }
        
       
        [HttpPost, Route("AddProductCategory"), AllowAnonymous]
        public async Task<IHttpActionResult> AddProductCategory(AddProductCategoryRequestDto productCAtegoryRequest)
        {
            try
            {
                using (var _context = new AdventureWorksSalesEntities())
                {
                    var categoryCheck = await _context.ProductCategories.Where(d => d.Name.ToLower().Equals(productCAtegoryRequest.Name.ToLower())).FirstOrDefaultAsync();
                    if (categoryCheck != null)
                    {
                        return Ok(new Response { ResponseCode = "90", ResponseMessage = "This Product Category Already Exist, please try again." });
                    }
                    else
                    {
                        var productCategory = new AdventureWorksSales.Core.ProductCategory()
                        {
                            ModifiedDate = DateTime.Now,
                            Name = productCAtegoryRequest.Name,
                            rowguid = Guid.NewGuid(),
                        };
                        _context.ProductCategories.Add(productCategory);
                        _context.SaveChanges();
                        return Ok(new Response { ResponseCode = "00", ResponseMessage = "Category Added Successfully" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured, reason: {ex.Message}");
            }
        }


        [HttpPut, Route("UpdateCategory"), AllowAnonymous]
        public async Task<IHttpActionResult> UpdateCategory(UpdateProductCategoryRequestDto updateRequest)
        {
            using (var _context = new AdventureWorksSalesEntities())
            {
                var res = await _context.ProductCategories.Where(f => f.ProductCategoryID == updateRequest.ProductCategoryID).FirstOrDefaultAsync();
                if (res == null) //go ahead and update
                {
                    return Ok(new Response { ResponseCode = "90", ResponseMessage = "No record(s) found", Data = null });
                }
                else
                {
                    _context.Database.BeginTransaction();
                    res.ModifiedDate = DateTime.Now;
                    res.Name = updateRequest.Name.Trim();
                    _context.Database.CurrentTransaction.Commit();
                    var res1 = await _context.SaveChangesAsync();
                    return Ok(new Response { Data = res1, ResponseCode = "00", ResponseMessage = "Category Updated Successfully" });
                }
            }
        }


        [HttpGet, Route("GetDashBoardDetails"), AllowAnonymous]
        public async Task<IHttpActionResult> GetDashBoardDetails()
        {
            try
            {
                using (var _context = new AdventureWorksSalesEntities())
                {
                    List<SalesOrder> sales = await _context.SalesOrders.ToListAsync();
                    var dashboardItems = new GetDashBoardItemsResponseDto
                    {
                        TotalOrders = sales.Select(d => d.OrderQty).Count(),
                        HighestLineTotal = sales.Max(f => f.LineTotal),
                        FrontBrakeTotalSales = sales.Join(_context.Products, w1 => w1.ProductID, w2 => w2.ProductID, (w1, w2) => new
                        {
                            w1.SalesOrderID,
                            w2.ProductID
                        }).ToList().Where(r => r.ProductID == 948).Count(),
                    };
                    return Ok(new Response<GetDashBoardItemsResponseDto> { DashBoardItems = dashboardItems, ResponseCode = "00", ResponseMessage = "Command(s) Completed Successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occured, reason: {ex.Message}");
            }
        }

    }
}
