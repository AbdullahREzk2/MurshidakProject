
namespace Project.BLL.Services
{
    public class LevelService:ILevelService
    {
        private readonly IMapper _mapper;
        private readonly ILevelRepository _levelRepository;

        public LevelService(IMapper mapper,ILevelRepository levelRepository)
        {
            _mapper = mapper;
            _levelRepository = levelRepository;
        }

        public async Task<IEnumerable<LevelDTO>> GetAllLevelsAsync()
        {
            var levels = await _levelRepository.GetAllLevelsAsync();
            return _mapper.Map<IEnumerable<LevelDTO>>(levels);
        }



    }
}
