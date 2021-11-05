namespace Moonpig.PostOffice.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Model;

    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        private DateTime maxLeadTime;
        private readonly DbContext dbContext;

        public DespatchDateController()
        {
             dbContext = new DbContext();
        }

        [HttpGet]
        public DespatchDate Get(List<int> productIds, DateTime orderDate)
        {
            maxLeadTime = orderDate;

            try
            {
                foreach (var productId in productIds)
                {
                    var supplierId = dbContext.Products.Single(x => x.ProductId == productId).SupplierId;
                    var leadTime = dbContext.Suppliers.Single(x => x.SupplierId == supplierId).LeadTime;

                    if (orderDate.AddDays(leadTime) > maxLeadTime)
                        maxLeadTime = orderDate.AddDays(leadTime);
                }
            }
            catch
            {
                // do something here to account for the linq queries
            }
            

            switch (maxLeadTime.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return new DespatchDate { Date = maxLeadTime.AddDays(2) };
                case DayOfWeek.Sunday:
                    return new DespatchDate { Date = maxLeadTime.AddDays(1) };
                default:
                    return new DespatchDate { Date = maxLeadTime };
            }
            
        }
    }
}
