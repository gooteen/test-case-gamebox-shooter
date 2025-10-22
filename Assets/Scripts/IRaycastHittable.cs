using UnityEngine;

public interface IRaycastHittable
{
    void OnRayHit(RaycastHit hitInfo, Weapon weapon);
}
