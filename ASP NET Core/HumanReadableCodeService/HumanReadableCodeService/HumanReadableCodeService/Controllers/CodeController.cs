using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HumanReadableCodeService.Services.Storage;

namespace HumanReadableCodeService.Controllers
{
    [Produces("application/json")]
    [Route("api/Code")]
    public class CodeController : Controller
    {
        private IHumanReadableCodeGenerator _generator;

        public CodeController(IHumanReadableCodeGenerator generator)
        {
            _generator = generator;
            _generator.GenerateRecords();
        }

        [HttpGet]
        [Route("All")]
        public List<string> Get([FromServices]IHumanReadableCodeGenerator ordersList)
        {

            return _generator.GetAllRecords();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var record = _generator.GetRecordById(id);

            if (record == null)
                return NotFound();

            return Ok(record);
        }


    }
}