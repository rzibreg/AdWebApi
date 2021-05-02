using AdWebApi.Business;
using AdWebApi.Database;
using AdWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AdWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryDataController : ControllerBase
    {
        private SimpleDatabase __database;

        public InventoryDataController()
        {
            __database = SimpleDatabase.GetInstance();
        }

        /// <summary>
        /// Create new inventory entry
        /// </summary>
        /// <param name="inventoryData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Save([FromBody] InventoryData inventoryData)
        {
            inventoryData.DecodedTags = inventoryData.Tags.Select(Sgtin96Decoder.ConvertFromHex).ToList();
            __database.SaveInventoryData(inventoryData);

            return Ok();
        }

        /// <summary>
        /// Create new inventory entry with list parameter
        /// </summary>
        /// <param name="inventoryData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SaveList([FromBody] List<InventoryData> inventoryData)
        {
            foreach (var inventory in inventoryData)
                inventory.DecodedTags = inventory.Tags.Select(Sgtin96Decoder.ConvertFromHex).ToList();

            __database.SaveInventoryData(inventoryData);

            return Ok();
        }
    }
}
