using System.Security.Claims;
using Web_API_.NET_7.Data;
using Web_API_.NET_7.Models;

namespace Web_API_.NET_7.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newChar)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newChar);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
                .Where(c => c.User!.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(dbCharacters);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id && x.User!.Id == GetUserId());            
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedChar)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == updatedChar.Id);
                if (character is null)
                    throw new Exception($"Character with id = '{updatedChar.Id}' was not found.");

                character.Name = updatedChar.Name;
                character.HitPoints = updatedChar.HitPoints;
                character.Defense = updatedChar.Defense;
                character.Strength = updatedChar.Strength;
                character.Intelligence = updatedChar.Intelligence;
                character.Class = updatedChar.Class;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id);
                if (character is null)
                    throw new Exception($"Character with id = '{id}' was not found.");

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<List<GetCharacterDto>>(await _context.Characters.ToListAsync());
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
