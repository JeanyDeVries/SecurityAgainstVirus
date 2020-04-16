using UnityEngine;

[CreateAssetMenu(fileName = "VirusProps", menuName = "ScriptableObjects/VirusScriptables/VirusProps")]
public class VirusProps : ScriptableObject
{
    [Header("Values")]
    public float spotRange;
    public float attackRange;
    public float movementSpeed;
    public float damage;
    public float attackCooldown;
    public float restorationTime;
    public float turnSpeed;

    [Header("Sounds")]
    public AudioClip deathSound;

    [Header("Effects")]
    public ParticleSystem deathEffect;
}
