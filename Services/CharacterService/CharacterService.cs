using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using Microsoft.AspNetCore.Hosting.Server;
using System;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id = 1, Name = "Lae'Zel", Class = RpgClass.Fighter},
        };

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context) 
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> CreateCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(x => x.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;  
        }

        public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
            var character = characters.FirstOrDefault(c => c.Id == id);
            if(character is null)
                    throw new Exception($"Character with Id {id} doesn't exist");

                characters.Remove(character);
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
                serviceResponse.Message = $"Character with Id {id} deleted";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            if (dbCharacter is null)
            {
                serviceResponse.Message = "No character found";
                serviceResponse.Success = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> UpdateCharacter(Character editCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                int index = characters.FindIndex(c => c.Id == editCharacter.Id);
                if (index == -1)
                    throw new Exception($"Character with Id {editCharacter.Id} doesn't exist");

                characters[index] = editCharacter;
                serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                serviceResponse.Message = $"Character with Id {editCharacter.Id} has been Updated";
            } catch (Exception ex) 
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
