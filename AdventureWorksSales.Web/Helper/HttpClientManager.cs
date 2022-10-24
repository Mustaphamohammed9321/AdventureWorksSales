using AdventureWorkSales.API.Helper;
using AdventureWorksSales.Core.Dtos.ResponseDto;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;

namespace AdventureWorksSales.Web.Helper
{
    public class HttpClientManager
    {
        AppSettingsFactory appSettingsFactory = new AppSettingsFactory();
        public async Task<Response<GetDashBoardItemsResponseDto>> GetAllTaskAsync(string endpointName)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(_config["TestDetails:TestUri"]); //add production uri to appsettings
                client.BaseAddress = new Uri(appSettingsFactory.GetBaseUrl()); //add production uri to appsettings
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync($"{client.BaseAddress}/{endpointName}");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<Response<GetDashBoardItemsResponseDto>>(results);
                    return response;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<GenericResponse> GetAllProductCatoriesAsync(string endpointName)
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(_config["TestDetails:TestUri"]); //add production uri to appsettings
                client.BaseAddress = new Uri(appSettingsFactory.GetBaseUrl()); //add production uri to appsettings
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync($"{client.BaseAddress}/{endpointName}");
                if (getData.IsSuccessStatusCode)
                {
                    string results = getData.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<GenericResponse>(results);
                    return response;
                }
                else
                {
                    return null;
                }
            }
        }


        public async Task<ProductCategoryResponse> GetByIdTaskAsync(string endpointName = "GetProductCategoryById?id=", int? ProductCategoryID = 0)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //client.BaseAddress = new Uri(_config["TestDetails:TestUri"]); //add production uri to appsettings
                    client.BaseAddress = new Uri(appSettingsFactory.GetBaseUrl()); //add production uri to appsettings
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage getData = await client.GetAsync($"{client.BaseAddress}/{endpointName}{ProductCategoryID}");
                    if (getData.IsSuccessStatusCode)
                    {
                        string results = getData.Content.ReadAsStringAsync().Result;
                        var category = JsonConvert.DeserializeObject<ProductCategoryResponse>(results);
                        return category;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Response> UpdateTaskAsync(int productCategoryId, Core.ProductCategory categoryRequest)
        {
            try
            {
                #region notused
                using (var client = new HttpClient())
                {
                    var productCat = JsonConvert.SerializeObject(categoryRequest.ProductCategoryID);

                    var requestContent = new StringContent(productCat, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(appSettingsFactory.GetBaseUrl()); //add production uri to appsettings
                    //var uri = Path.Combine("companies", "fc12c11e-33a3-45e2-f11e-08d8bdb38ded");
                    var response = await client.PutAsync(client.BaseAddress + "/UpdateCategory", requestContent);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        return new Response
                        {
                            ResponseCode = "00",
                            ResponseMessage = "Updated Successfully"
                        };
                    }
                    else
                    {
                        return new Response
                        {
                            ResponseCode = "90",
                            ResponseMessage = "An error occured"
                        };
                    }
                }
                #endregion
                #region MyRegion
                //using (var client = new RestClient($"{appSettingsFactory.GetBaseUrl()}/UpdateCategory"))
                //{
                //    var request = new RestRequest("Put", Method.Put);
                //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //    request.AddHeader("Accept", "application/json");
                //    request.AddParameter("ProductCategoryID", productCategoryId);
                //    request.AddParameter("Name", categoryRequest.Name);
                //    var response = await client.ExecuteAsync(request);
                //    if (response.IsSuccessStatusCode)
                //    {
                //        return new Response { ResponseCode = "00", ResponseMessage = "Updated Successfully" };
                //    }
                //    else
                //    {
                //        return new Response { ResponseCode = "90", ResponseMessage = "An error occured" };
                //    }
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                return new Response { ResponseCode = "99", ResponseMessage = $"An error occured, reason: {ex.Message}" };
            }
        }


        public async Task<Response> AddProductCategoryTaskAsync(Core.ProductCategory productCategory)
        {

            using (var client = new RestClient($"{appSettingsFactory.GetBaseUrl()}/AddProductCategory"))
            {
                var request = new RestRequest("Post", method: Method.Post);
                //request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                request.AddParameter("Name", productCategory.Name);
                var response = await client.ExecutePostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return new Response { ResponseCode = "00", ResponseMessage = "Category Added Successfully" };
                }
                else
                {
                    return new Response { ResponseCode = "90", ResponseMessage = "An error Occured, please try again" };
                }
            }
        }

    }
}