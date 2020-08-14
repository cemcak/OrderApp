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
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IGenericService<Order> _genericServiceOrder;
        private readonly IGenericService<Product> _genericServiceProduct;
        private readonly IGenericService<OrderProduct> _genericServiceOrderProduct;

        public OrderController(IGenericService<Order> genericServiceOrder, IGenericService<Product> genericServiceProduct, IGenericService<OrderProduct> genericServiceOrderProduct)
        {
            _genericServiceOrder = genericServiceOrder;
            _genericServiceProduct = genericServiceProduct;
            _genericServiceOrderProduct = genericServiceOrderProduct;
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
                    var productArray = new JArray();

                    foreach (var item in result)
                    {
                        productArray.Add(item);
                    }
                    returnModel["data"] = productArray;
                }

                return Ok(returnModel);
            } catch (Exception ex)
            {
                returnModel["error"] = true;
                returnModel["message"] = ex.Message;
                return BadRequest(returnModel);
            }
        }

        [HttpPost, Route("create")]
        public async Task<IActionResult> Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var returnModel = new JObject();
            returnModel["error"] = false;
            returnModel["message"] = "";
            try
            {
                if (order.OrderProducts.Count > 0)
                {
                    foreach (var item in order.OrderProducts)
                    {
                        var productInOrder = await _genericServiceProduct.GetAsync(x => x.Id == item.ProductId && x.Active == true && x.Deleted == false);
                        var orderDetails = await _genericServiceOrder.GetAsync(x => x.Id == item.OrderId && x.Active == true && x.Deleted == false);

                        if (productInOrder == null)
                        {
                            returnModel["error"] = true;
                            returnModel["message"] = "Product unavailable now";
                            return BadRequest(returnModel);
                        }

                        if (productInOrder.InStock < orderDetails.Quantity)
                        {
                            returnModel["error"] = true;
                            returnModel["message"] = "Sorry, we don't have enough " + productInOrder.Name + " in stock.";
                            return BadRequest(returnModel);
                        }
                    }
                }

                await _genericServiceOrder.AddAsync(order);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}