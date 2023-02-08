using AutoMapper;
using GamesAPI.Data.UnitOfWork;
using GamesAPI.Models;
using GamesAPI.Models.DTO;

namespace GamesAPI.Services
{
    public class TeamService : ITeamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Any(int id)
        {
            return await _unitOfWork.TeamRepository.Any(x => x.Id == id);
        }

        public async Task<TeamDto> Get(int id)
        {
            var team = await _unitOfWork.TeamRepository.Get(id);
            var dto = _mapper.Map<TeamDto>(team);
            return dto;
        }

        public async Task<IEnumerable<TeamDto>> GetAll()
        {
            var teams = await _unitOfWork.TeamRepository.GetAll();
            var dto = _mapper.Map<IEnumerable<TeamDto>>(teams);
            return dto;
        }


        public async Task<bool> Create(TeamCreateDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            try
            {
                _unitOfWork.TeamRepository.Add(team);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }

        }

        public async Task<bool> Update(TeamDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            try
            {
                _unitOfWork.TeamRepository.Update(team);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> Delete(int id)
        {
            var team = await _unitOfWork.TeamRepository.Get(id);
            try
            {
                if (team != null)
                {
                    _unitOfWork.TeamRepository.Delete(team);
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
