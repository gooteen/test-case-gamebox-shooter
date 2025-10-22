using UnityEngine;

public class SingleShotWeapon : Weapon
{
    public override void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && CanFire())
        {
            FireRay();
            ResetFireCooldown();
        }
    }
}
