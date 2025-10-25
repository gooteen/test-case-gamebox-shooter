using UnityEngine;

public class AutoWeapon : Weapon
{
    [SerializeField] private Animator _anim;

    protected override void Start()
    {
        base.Start();
    }
    public override void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                FireRay();
                ResetFireCooldown();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("heey");
            _anim.SetBool("PlayHandShake", true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _anim.SetBool("PlayHandShake", false);
        }
    }
}