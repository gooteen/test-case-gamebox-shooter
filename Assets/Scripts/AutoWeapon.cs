using UnityEngine;

public class AutoWeapon : Weapon
{
    public override void Shoot()
    {
        if (Input.GetMouseButton(0) && CanFire())
        {
            FireRay();
            ResetFireCooldown();
        }
    }
}