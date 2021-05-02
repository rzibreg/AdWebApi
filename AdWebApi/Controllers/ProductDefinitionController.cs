using AdWebApi.Database;
using AdWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AdWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDefinitionController : ControllerBase
    {
        private SimpleDatabase __database;

        public ProductDefinitionController()
        {
            __database = SimpleDatabase.GetInstance();
        }

        /// <summary>
        /// Create new product definition
        /// </summary>
        /// <param name="productDefinition"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Save([FromBody] ProductDefinition productDefinition)
        {
            __database.SaveProductDefinition(productDefinition);

            return Ok();
        }

        /// <summary>
        /// Create new product definition with list parameter
        /// </summary>
        /// <param name="productDefinitions"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveList([FromBody] List<ProductDefinition> productDefinitions)
        {
            __database.SaveProductDefinition(productDefinitions);

            return Ok();
        }
    }
}
