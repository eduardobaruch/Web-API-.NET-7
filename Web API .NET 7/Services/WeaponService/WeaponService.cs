﻿using System.Security.Claims;
using Web_API_.NET_7.Data;

namespace Web_API_.NET_7.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && 
                        c.User!.Id == GetUserId());

                if (character is null)
                    throw new Exception("Character not found.");

                if (character.Weapon is not null)
                    throw new Exception("Character already have a weapon");

                var weapon = new Weapon()
                {
                    Name = newWeapon.Name,
                    Demage = newWeapon.Demage,
                    Character = character
                };
                
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
            }
            
            return response;
        }
    }
}