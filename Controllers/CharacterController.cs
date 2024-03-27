global using dotnet_rpg.Models;
using dotnet_rpg.Dtos.Character;
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
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }    

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetById(int id) 
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost()]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> CreateCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.CreateCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> EditCharacter (UpdateCharacterDto character)
        {
            var response = await _characterService.UpdateCharacter(character);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacterById(int id)
        {
            var response = await _characterService.DeleteCharacterById(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
