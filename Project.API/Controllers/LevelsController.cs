
namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        public readonly ILevelService _levelService;
        public LevelsController(ILevelService levelService)
        {
            _levelService = levelService;
        }

        #region get all levels
        [HttpGet("getAllLevels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllLevels()
        {
            var levels = await _levelService.GetAllLevelsAsync();
            if(levels == null)
            {
                return NotFound("No levels found.");
            }   
            return Ok(levels);
        }
        #endregion

    }
}
