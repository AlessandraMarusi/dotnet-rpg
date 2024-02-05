
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id = 1, Name = "Lae'Zel", Class = RpgClass.Fighter},
        };

        public async Task<ServiceResponse<List<Character>>> CreateCharacter(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(character);
            serviceResponse.Data = characters;
            return serviceResponse;  
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = characters;

            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(x => x.Id == id);
            var serviceResponse = new ServiceResponse<Character>();
            serviceResponse.Data = character;
            if (character is null)
            {
                serviceResponse.Message = "No character found";
                serviceResponse.Success = false;

            }
            return serviceResponse;
        }
    }
}
