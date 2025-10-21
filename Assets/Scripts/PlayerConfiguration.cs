using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject
{
    public float walkingSpeed = 6f;
    public float sprintingSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public float airSpeedDampMultiplier = 0.25f;
}
