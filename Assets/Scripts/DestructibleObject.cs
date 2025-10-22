using UnityEngine;
using System.Collections.Generic;

public class DesturctibleObject : MonoBehaviour, IRaycastHittable
{
    [SerializeField] private List<Rigidbody> _objectFractions;
    private BoxCollider _col;
    
    void Awake()
    {
        _col = GetComponent<BoxCollider>();
    }
    public void OnRayHit(RaycastHit hit, Weapon weapon)
    {
        Explode();
    }

    private void Explode()
    {
        foreach (Rigidbody fraction in _objectFractions)
        {
            fraction.isKinematic = false;
            fraction.AddForce((fraction.transform.position - this.transform.position) * Random.Range(10f, 30f), ForceMode.Impulse);
        }

        _col.enabled = false;
    }
}
