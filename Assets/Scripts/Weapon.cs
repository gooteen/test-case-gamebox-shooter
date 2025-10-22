using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Base Weapon Settings")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    public GameObject hitEffectPrefab;

    [Header("Debug Settings")]
    public bool showDebugRay = true;
    public Color debugRayColor = Color.red;
    public float debugRayDuration = 0.1f;

    protected float nextFireTime = 0f;
    protected Camera playerCamera;

    protected virtual void Start()
    {
        playerCamera = Camera.main;
    }

    public abstract void Shoot();

    protected void FireRay()
    {
        // Fire effect
        if (muzzleFlash != null)
            muzzleFlash.Play();

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (showDebugRay)
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * range, debugRayColor, debugRayDuration);
        }

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log($"Hit: {hit.collider.name}", hit.collider);

            IRaycastHittable hittable = hit.collider.GetComponent<IRaycastHittable>();
            if (hittable != null)
            {
                hittable.OnRayHit(hit, this);
            }

            // Impact effect
            if (hitEffectPrefab != null)
            {
                Quaternion hitRotation = Quaternion.LookRotation(hit.normal);
                GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point + hit.normal * 0.001f, hitRotation);
                Destroy(hitEffect, 2f);
            }
        }
    }

    protected bool CanFire()
    {
        return Time.time >= nextFireTime;
    }

    protected void ResetFireCooldown()
    {
        nextFireTime = Time.time + fireRate;
    }

}