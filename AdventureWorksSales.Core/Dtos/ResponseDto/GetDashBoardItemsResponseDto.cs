using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksSales.Core.Dtos.ResponseDto
{
    public class GetDashBoardItemsResponseDto
    {
        public float FrontBrakeTotalSales { get;set;}
        public int TotalOrders { get;set;}
        public decimal HighestLineTotal { get;set;}
    }
}
