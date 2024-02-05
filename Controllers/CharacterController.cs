global using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;

using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }    

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetById(int id) 
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> CreateCharacter(Character character)
        {
            return Ok(await _characterService.CreateCharacter(character));
        }
    }
}
