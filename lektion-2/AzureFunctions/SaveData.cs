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
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AzureFunctions
{
    public static class SaveData
    {
        [FunctionName("SaveData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            var model = JsonConvert.DeserializeObject<ProductCreateModel>(await new StreamReader(req.Body).ReadToEndAsync());

            using(IDbConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnection")))
            {
                conn.Open();
                conn.Execute("INSERT INTO Products VALUES(@Name,@Description,@Price,@CategoryId)", model);
            }

            return new OkResult();
        }
    }
}
