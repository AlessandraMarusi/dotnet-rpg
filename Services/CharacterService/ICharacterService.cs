using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<Character>> GetCharacterById(int id);
        Task<ServiceResponse<List<Character>>> GetAllCharacters();
        Task<ServiceResponse<List<Character>>> CreateCharacter(Character character);
    }
}
