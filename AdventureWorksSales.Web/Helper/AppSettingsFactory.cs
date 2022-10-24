using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AdventureWorksSales.Web.Helper
{
    public class AppSettingsFactory
    {
        public string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings["BaseUrl"].ToString();
        } 
        
        
        public string GetFrontEndBaseUrl()
        {
            return ConfigurationManager.AppSettings["FrontEndBaseUrl"].ToString();
        } 
        
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        } 



    }
}