using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ItemBAL.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductBLL.Repositories;
using ProductBLL.Services;

namespace ProductCatalogAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    //[Produces("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly IDataRepository<ItemVM> Repository;
        public static IHostingEnvironment _environment;

        public ItemsController(IDataRepository<ItemVM> _Repository, IHostingEnvironment environment)
        {
            Repository = new ItemSerivce();
            Repository = _Repository;
            _environment = environment;

        }
        // GET api/Item
        [HttpGet]
        [ProducesResponseType(typeof(ItemVM), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var entity = await Repository.GetAll();
            if (entity != null)
            {
                return Ok(entity);
            }
            return NoContent();
        }

        // GET api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetById(int id)
        {
            try
            {
                if (id!=0)
                {
                    var entity=await Repository.Get(id);
                    return Ok(entity);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // POST api/Item
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreatProductFromSwagger([FromForm] ItemVM item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var image = Request.Form.Files[0];
                    var entity = new ItemVM()
                    {
                        Title = item.Title,
                        Description = item.Description
                
                    };
                    Repository.Add(entity);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddItem([FromBodyAttribute] ItemVM item)
        {
            try
            {
                var entity = new ItemVM()
                {
                    Title = item.Title,
                    Description = item.Description,
                };
                Repository.Add(entity);
                return Ok();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public async Task<ActionResult> Put()
        {
            try
            {
                string itemValues = Request.Form["info"];
                var item = JsonConvert.DeserializeObject<ItemVM>(itemValues);
                //DeleteOldPhoto(product.Id);
                if (item != null)
                {
                    var entity = new ItemVM()
                    {
                        Title = item.Title,
                        Description = item.Description,
                    };
                    Repository.Add(entity);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id!=0)
                {
                    Repository.Delete(id);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

 
    }
}
