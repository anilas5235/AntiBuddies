using Project.Scripts.WeaponSystem.Slot;

namespace Project.Scripts.WeaponSystem
{
    public interface IWeapon 
    {
        public void SetWeaponSlot(WeaponSlot weaponSlot);
        public void Attack();
        public void DestroyWeapon();
    }
}