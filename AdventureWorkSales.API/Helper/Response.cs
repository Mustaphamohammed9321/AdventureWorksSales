using AdventureWorksSales.Core;
using AdventureWorksSales.Core.Dtos.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorkSales.API.Helper
{
    public class Response<T>
    {
        public GetDashBoardItemsResponseDto DashBoardItems { get; set; }
        public List<ProductCategory> CategoryList { get; set; }
        public object Data { get; set; }
        public object Data2 { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
    }
    
    public class Response
    {
        public object Data { get; set; }
        public object Data2 { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
    }
    
    public class ProductCategoryResponse
    {
        public ProductCategory Data { get; set; }
        public object Data2 { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
    }
}