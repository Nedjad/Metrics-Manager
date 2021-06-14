using HomeWork__1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork__1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly Temperature _temperatureModel;

        public CrudController(Temperature temperatureModel)
        {
            _temperatureModel = temperatureModel;
        }

        
        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _temperatureModel.AddValue(date, temperature);
            return Ok();
        }
        
        [HttpGet("read_all")]
        public IActionResult ReadAll()
        {
            return Ok(_temperatureModel.GetTemperatureValues());
        }
        
        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(_temperatureModel.GetTemperatureValues(from, to));
        }
        
        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temperature)
        {
            _temperatureModel.UpdateValue(date, temperature);
            return Ok();
        }
        
        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date)
        {
            _temperatureModel.DeleteValue(date);
            return Ok();
        }
        
        [HttpDelete("delete_range")]
        public IActionResult Delete([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            _temperatureModel.DeleteRange(from, to);
            return Ok();
        }
    }
}
