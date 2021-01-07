using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using RickLocalization_api.EF;
using RickLocalization_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RickLocalization_api.v1.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public class MotyController : ControllerBase
    {
        public readonly IMotyService _repository;
        
        public MotyController(IMotyService repository)
        {
            _repository = repository;
        }

        [HttpGet ("ListAll")]
        public async Task<IActionResult> ListAll () {
            try {
                var results = await _repository.ListAsync();

                return Ok (results);
            } catch (System.Exception ex) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost ("Add")]
        public async Task<IActionResult> Add (Morty model) {
            try {
                _repository.Add (model);

                if (await _repository.SaveChangesAsync ()) {
                     return Ok (model);
                }
            } catch (System.Exception ex) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest ();
        }

        [HttpPost ("Update")]
        public async Task<IActionResult> Update (Morty model) {
            try {
                _repository.Update(model);

                if (await _repository.SaveChangesAsync ()) {
                    return Ok (model);
                }
            } catch (System.Exception ex) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest ();
        }

        [HttpDelete ("Delete")]
        public async Task<IActionResult> Delete (Morty model) {
            try {
                _repository.Delete(model);

                if (await _repository.SaveChangesAsync ()) {
                    return Ok (model);
                }
            } catch (System.Exception ex) {
                return this.StatusCode (StatusCodes.Status500InternalServerError, ex.Message);
            }

            return BadRequest ();
        }


    }
}