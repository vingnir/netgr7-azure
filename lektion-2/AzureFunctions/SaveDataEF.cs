using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Models;

namespace AzureFunctions
{
    public static class SaveDataEF
    {
        [FunctionName("SaveDataEF")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var _context = new SqlContext();
            var model = JsonConvert.DeserializeObject<ProductCreateModel>(await new StreamReader(req.Body).ReadToEndAsync());

            _context.Products.Add(new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId
            });
            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}
