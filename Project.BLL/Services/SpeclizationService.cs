

namespace Project.BLL.Services
{
    public class SpeclizationService : ISpeclizationService
    {
        private readonly IMapper _mapper;
        private readonly ISpeclizationRepository _speclizationRepository;

        public SpeclizationService(IMapper mapper,ISpeclizationRepository speclizationRepository)
        {
            _mapper = mapper;
            _speclizationRepository = speclizationRepository;
        }
        public async Task<IEnumerable<SpeclizationDTO>> GetAllSpecializationsAsync()
        {
            var speclizations = await _speclizationRepository.GetAllSpecializationsAsync();
            return _mapper.Map<IEnumerable<SpeclizationDTO>>(speclizations);
        }

    }
}
