using UnityEngine;

[CreateAssetMenu(fileName = "VirusProps", menuName = "ScriptableObjects/VirusScriptables/VirusProps")]
public class VirusProps : ScriptableObject
{
    /*
     * These are the values which can be changed on every virus 
     * in the scriptable object
     */

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
    public AudioClip attackSound;

    [Header("Effects")]
    public ParticleSystem deathEffect;
}
