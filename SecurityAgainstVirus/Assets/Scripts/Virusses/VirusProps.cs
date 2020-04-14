using UnityEngine;

[CreateAssetMenu(fileName = "VirusProps", menuName = "ScriptableObjects/VirusScriptables/VirusProps")]
public class VirusProps : ScriptableObject
{
    public float spotRange, attackRange,
        movementSpeed, damage, attackCooldown,
        restorationTime, turnSpeed;
    public AudioClip deathSound;
}
