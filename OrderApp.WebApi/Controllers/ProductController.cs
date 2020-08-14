using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderApp.Business.Interfaces;
using OrderApp.Entities.Concrete;

namespace OrderApp.WebApi.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _genericServiceProduct;

        public ProductController(IGenericService<Product> genericServiceProduct)
        {
            _genericServiceProduct = genericServiceProduct;
        }

        [Route("get_all_available_async")]
        public async Task<IActionResult> GetAllAvailableAsync()
        {
            var returnModel = new JObject();
            try
            {
                var result = await _genericServiceProduct.GetAllAsync(x => x.Active == true && x.Deleted == false && x.InStock > 0);

                if (result.Count > 0)
                {
                    var productArray = JArray.FromObject(result);
                    returnModel["data"] = productArray;
                }

                return Ok(returnModel);
            }
            catch (Exception ex)
            {
                returnModel["error"] = true;
                returnModel["message"] = ex.Message;
                return BadRequest(returnModel);
            }
        }
    }
}