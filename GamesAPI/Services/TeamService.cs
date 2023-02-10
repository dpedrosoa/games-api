using AutoMapper;
using GamesAPI.Data.UnitOfWork;
using GamesAPI.Models;
using GamesAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<PlayerDto>> GetPlayersByTeam(int teamId)
        {
            List<PlayerDto> playersDto = new List<PlayerDto>();
            var team = (await _unitOfWork.TeamRepository
                .GetAll(
                    filter: x => x.Id == teamId,
                    include: x => x.Include(t => t.TeamPlayers).ThenInclude(p => p.Player)
                    )
                ).FirstOrDefault();

            if(team != null)
            {
                var players = team.TeamPlayers.Select(x => x.Player);
                playersDto = _mapper.Map<List<PlayerDto>>(players);
            }

            return playersDto;
        }

        public async Task<bool> AddPlayerToTeam(int teamId, int playerId)
        {
            try
            {
                var teamPlayer = await _unitOfWork.TeamPlayerRepository.Get(teamId, playerId);
                if(teamPlayer == null)
                {
                    _unitOfWork.TeamPlayerRepository.Add(new TeamPlayer { TeamId = teamId, PlayerId = playerId });
                    return await _unitOfWork.Save();
                }
                else // player is already added to the team
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            #region Other option that updates two entries (Team and TeamPlayer)

            //var team = (await _unitOfWork.TeamRepository
            //    .GetAll(
            //        filter: x => x.Id == teamId,
            //        include: x => x.Include(t => t.TeamPlayers).ThenInclude(p => p.Player),
            //        disabledTracking: false
            //        )
            //    ).FirstOrDefault();

            //try
            //{
            //    if (team != null)
            //    {
            //        // already exists
            //        if (team.TeamPlayers.Select(x => x.PlayerId).Contains(playerId))
            //            return true;

            //        team.TeamPlayers.Add(new TeamPlayer { TeamId = teamId, PlayerId = playerId });
            //        _unitOfWork.TeamRepository.Update(team);
            //        return await _unitOfWork.Save();
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}

            #endregion

        }

        public async Task<bool> DeletePlayerFromTeam(int teamId, int playerId)
        {
            try
            {
                var teamPlayer = await _unitOfWork.TeamPlayerRepository.Get(teamId, playerId);
                if(teamPlayer != null)
                {
                    _unitOfWork.TeamPlayerRepository.Delete(teamPlayer);
                    return await _unitOfWork.Save();
                }
                else // player is already deleted from team
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            #region Other option that updates two entries (Team and TeamPlayer)

            //var team = (await _unitOfWork.TeamRepository
            //    .GetAll(
            //        filter: x => x.Id == teamId,
            //        include: x => x.Include(t => t.TeamPlayers).ThenInclude(p => p.Player),
            //        disabledTracking: false
            //        )
            //    ).FirstOrDefault();

            //try
            //{
            //    if (team != null)
            //    {
            //        var tPlayer = team.TeamPlayers.FirstOrDefault(x => x.PlayerId == playerId);
            //        if (tPlayer != null)
            //        {
            //            team.TeamPlayers.Remove(tPlayer);
            //            _unitOfWork.TeamRepository.Update(team);
            //            return await _unitOfWork.Save();
            //        }
            //        else // player is not on team
            //        {
            //            return true;
            //        }
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}

            #endregion

        }
    }
}
