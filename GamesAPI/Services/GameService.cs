﻿using AutoMapper;
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
                //include: x => x.Include(x => x.GameTeams).ThenInclude(x => x.Team),
                //orderBy: x => x.OrderBy(x => x.Name)
                //);

            var dto =_mapper.Map<IEnumerable<GameDto>>(games);

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

    }
}
