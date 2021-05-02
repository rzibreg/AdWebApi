using AdWebApi.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AdWebApi.Controllers
{
    [Route("api/Count")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IDataFilter __dataFilter;

        public StockController(IDataFilter dataFilter)
        {
            __dataFilter = dataFilter;
        }

        /// <summary>
        /// Count by a specific product for a specific inventory
        /// </summary>
        /// <param name="itemReference"></param>
        /// <param name="inventoryId"></param>
        /// <returns>Count</returns>
        /// <response code="200">Returns count by a specific product for a specific inventory</response>
        /// <response code="400">Returns error</response>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public int ByProductAndInventory([FromQuery] string itemReference, [FromQuery] string inventoryId)
        {
            return __dataFilter.CountByProductAndInventory(itemReference, inventoryId);
        }

        /// <summary>
        /// Count by a specific product per day
        /// </summary>
        /// <param name="itemReference"></param>
        /// <param name="dateOfInventory"></param>
        /// <returns>Count</returns>
        /// <response code="200">Returns count by a specific product per day</response>
        /// <response code="400">Returns error</response>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public int ByProductPerDay([FromQuery] string itemReference, [FromQuery] DateTime dateOfInventory)
        {
            return __dataFilter.CountByProductPerDay(itemReference, dateOfInventory);
        }

        /// <summary>
        /// Count by a specific company
        /// </summary>
        /// <param name="companyPrefix"></param>
        /// <returns>Count</returns>
        /// <response code="200">Returns count by a specific company</response>
        /// <response code="400">Returns error</response>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public int ByCompany([FromQuery] string companyPrefix)
        {
            return __dataFilter.CountByCompanyPrefix(companyPrefix);
        }
    }
}
