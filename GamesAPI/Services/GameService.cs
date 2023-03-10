using AutoMapper;
using GamesAPI.Data.UnitOfWork;
using GamesAPI.Models;
using GamesAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace GamesAPI.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<bool> Any(int id)
        {
            return await _unitOfWork.GameRepository.Any(x => x.Id == id);
        }

        public async Task<GameDto> Get(int id)
        {
            var game = await _unitOfWork.GameRepository.Get(id);
            var dto = _mapper.Map<GameDto>(game);
            return dto;
        }

        public async Task<IEnumerable<GameDto>> GetAll()
        {
            var games = await _unitOfWork.GameRepository.GetAll();

            var dto = _mapper.Map<IEnumerable<GameDto>>(games);
            return dto;
        }

        public async Task<bool> Create(GameCreateDto dto)
        {
            var game = _mapper.Map<Game>(dto);
            try
            {
                _unitOfWork.GameRepository.Add(game);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(GameDto dto)
        {
            var game = _mapper.Map<Game>(dto);
            try
            {
                _unitOfWork.GameRepository.Update(game);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var game = await _unitOfWork.GameRepository.Get(id);
            try
            {
                if (game != null)
                {
                    _unitOfWork.GameRepository.Delete(game);
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

        public async Task<List<TeamDto>> GetTeamsByGame(int gameId)
        {
            List<TeamDto> teamsDto = new List<TeamDto>();
            var game = (await _unitOfWork.GameRepository
                .GetAll(
                    filter: x => x.Id == gameId,
                    include: x => x.Include(g => g.GameTeams).ThenInclude(t => t.Team)
                )
                ).FirstOrDefault();

            if (game != null)
            {
                var teams = game.GameTeams.Select(x => x.Team);
                teamsDto = _mapper.Map<List<TeamDto>>(teams);
            }
            return teamsDto;
        }


        public async Task<List<GameTeamInfoDto>> GetTeamsInfoByGame(int gameId)
        {
            List<GameTeamInfoDto> teamsInfoDto = new List<GameTeamInfoDto>();
            var game = (await _unitOfWork.GameRepository
                .GetAll(
                    filter: x => x.Id == gameId,
                    include: x => x.Include(g => g.GameTeams).ThenInclude(t => t.Team)
                        .ThenInclude(tp => tp.TeamPlayers).ThenInclude(p => p.Player)
                )
                ).FirstOrDefault();

            if (game != null)
            {
                teamsInfoDto = game.GameTeams
                    .Select(x => new GameTeamInfoDto
                    {
                        TeamId = x.TeamId,
                        TeamName = x.Team.Name,
                        GameId = game.Id,
                        TeamScore = x.TeamScore,
                        Players = _mapper.Map<List<PlayerDto>>(x.Team.TeamPlayers.Select(x => x.Player))
                    })
                    .OrderByDescending(t => t.TeamScore)
                    .ToList();
            }
            return teamsInfoDto;
        }


        public async Task<bool> AddTeamToGame(int gameId, int teamId)
        {
            // Adds a new GameTeam entity to the context
            // Better performance, Only changes one entry to the database => GameTeam
            try
            {
                // check if team is already added to the game
                var gameTeam = await _unitOfWork.GameTeamRepository.Get(gameId, teamId);
                if(gameTeam == null)
                {
                    _unitOfWork.GameTeamRepository.Add(
                        new GameTeam { GameId = gameId, TeamId = teamId }
                    );

                    return await _unitOfWork.Save();
                }
                else // team is already added
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            // Adds a new GameTeam to the Game navigation property (GameTeams) and updates the Game
            // Saves two entries on the database => GameTeam and Game
            #region Other option that updates the Game and GameTeams

            //var game = (await _unitOfWork.GameRepository
            //    .GetAll(
            //        filter: x => x.Id == gameId,
            //        include: x => x.Include(g => g.GameTeams).ThenInclude(t => t.Team),
            //        disabledTracking: false
            //        )
            //    ).FirstOrDefault();

            //try
            //{
            //    if (game != null)
            //    {
            //        // already exists
            //        if (game.GameTeams.Select(x => x.TeamId).Contains(teamId))
            //            return true;

            //        game.GameTeams.Add(new GameTeam { GameId = gameId, TeamId = teamId });
            //        _unitOfWork.GameRepository.Update(game);
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

        public async Task<bool> DeleteTeamFromGame(int gameId, int teamId)
        {
            try
            {
                // check if team is already deleted from the game
                var gameTeam = await _unitOfWork.GameTeamRepository.Get(gameId, teamId);
                if(gameTeam != null) {
                    _unitOfWork.GameTeamRepository.Delete(gameTeam);
                    return await _unitOfWork.Save();
                }
                else // team is already deleted
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            // Removes existing GameTeam from the Game navigation property (GameTeams) and updates the Game
            // Saves two entries on the database => GameTeam and Game
            #region Other option that updates the Game and GameTeams

            //var game = (await _unitOfWork.GameRepository
            //    .GetAll(
            //        filter: x => x.Id == gameId,
            //        include: x => x.Include(g => g.GameTeams).ThenInclude(t => t.Team),
            //        disabledTracking: false
            //        )
            //    ).FirstOrDefault();

            //try
            //{
            //    if (game != null)
            //    {
            //        var gTeam = game.GameTeams.FirstOrDefault(x => x.TeamId == teamId);
            //        if (gTeam != null)
            //        {
            //            game.GameTeams.Remove(gTeam);
            //            _unitOfWork.GameRepository.Update(game);
            //            return await _unitOfWork.Save();
            //        }
            //        else // team is not on game
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

        

        public async Task<bool> UpdateTeamScore(GameTeamDto gameTeamDto)
        {
            var gameTeam = _mapper.Map<GameTeam>(gameTeamDto);
            try
            {
                _unitOfWork.GameTeamRepository.Update(gameTeam);
                return await _unitOfWork.Save();
            }
            catch
            {
                return false;
            }
        }
    }
}
