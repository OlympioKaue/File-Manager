using Files.Application.UseCases.Files.Delete;
using Files.Application.UseCases.Files.GetID;
using Files.Application.UseCases.Files.Register;
using Files.Application.UseCases.Files.Update;
using Files.Communication.Enums;
using Files.Communication.Requests;
using Files.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Files.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesManagerController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseFilesJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public IActionResult RegisterFiles([FromBody] RequestFilesJson register)
        {
            var useCases = new RegisterFilesUseCase();
            var responses = useCases.Execute(register);

            return Created(string.Empty, responses); // retorne 201
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status400BadRequest)]

        public IActionResult UpdateFilesID([FromRoute] int id, [FromBody] RequestFilesJson update)
        {
            var useCases = new UpdateFilesUseCase();

            useCases.Execute(id, update); 

            return NoContent(); // retorne 204
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllFilesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            
            var useCases = new GetAllFilesUseCase();
            var responses = useCases.Execute();
            
            if (responses.Files.Any())
            {
                // se response true me retorne a lista !

                return Ok(responses); // retorne 200
            }

            return NoContent(); // retorne 204 se false
    
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseFilesJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]

        public IActionResult GetID([FromRoute] int id)
        {
            var useCases = new GetIdFilesUseCase();
            var responses = useCases.Execute(id);

           // O Route substitui a lógica do if, o mesmo obriga o user digitalizar o id !


            return Ok(responses); // retorne 200
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete([FromRoute] int id)
        {
            var useCases = new DeleteFilesUseCase();
            useCases.Execute(id);

            return NoContent(); // retorne 204
        }


    }
}
