
namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeclizationController : ControllerBase
    {
        private readonly ISpeclizationService _speclizationService;

        public SpeclizationController(ISpeclizationService speclizationService)
        {
            _speclizationService = speclizationService;
        }

        #region Get All Speclizations
        [HttpGet("getAllSpeclizations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSpeclizations()
        {
            var speclizations = await _speclizationService.GetAllSpecializationsAsync();
            if(speclizations == null || !speclizations.Any())
            {
                return NotFound("No speclizations found.");
            }
            return Ok(speclizations);
        }
        #endregion

    }
}
