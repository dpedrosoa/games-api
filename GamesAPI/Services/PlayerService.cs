using AutoMapper;
using GamesAPI.Data.UnitOfWork;
using GamesAPI.Models;
using GamesAPI.Models.DTO;

namespace GamesAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlayerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Any(int id)
        {
            return await _unitOfWork.PlayerRepository.Any(x => x.Id == id);
        }

        public async Task<PlayerDto> Get(int id)
        {
            var player = await _unitOfWork.PlayerRepository.Get(id);
            var dto = _mapper.Map<PlayerDto>(player);
            return dto;
        }

        public async Task<IEnumerable<PlayerDto>> GetAll()
        {
            var players = await _unitOfWork.PlayerRepository.GetAll();
            var dto = _mapper.Map<IEnumerable<PlayerDto>>(players);
            return dto;
        }

        public async Task<bool> Create(PlayerCreateDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            try
            {
                _unitOfWork.PlayerRepository.Add(player);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> Update(PlayerDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            try
            {
                _unitOfWork.PlayerRepository.Update(player);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            var player = await _unitOfWork.PlayerRepository.Get(id);
            try
            {
                if(player != null)
                {
                    _unitOfWork.PlayerRepository.Delete(player);
                    return await _unitOfWork.Save();
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
