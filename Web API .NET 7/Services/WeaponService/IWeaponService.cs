global using Web_API_.NET_7.Dto.Weapon;

namespace Web_API_.NET_7.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}
