using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ), typeof( Rigidbody ) )]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float energyAbsorbPerSecond = 20f;
    [SerializeField]
    private float healthPoints = 5f;

    NavMeshAgent agent;
    PlayerController target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        agent.SetDestination( target.transform.position );
    }

    private void OnCollisionStay( Collision collision )
    {
        // If the enemy collide with the player
        if ( collision.gameObject.CompareTag("Player") )
        {
            Energizer energizer = collision.gameObject.GetComponentInChildren<Energizer>();

            if ( energizer != null )
            {
                // Enemy steal energy to the player
                energizer.GiveEnergy( energyAbsorbPerSecond * Time.deltaTime );
            }
        }
    }

    private void OnTriggerStay( Collider other )
    {
        // If the enemy stays in the area of the lightspot
        if ( other.CompareTag("Lightspot") )
        {
            EnergyAbsorber ea = other.GetComponent<EnergyAbsorber>();

            // Decrease enemy healtpoints by lightspot intensity power
            healthPoints-= Time.deltaTime * ea.Intensity;

            // If enemy has no healthpoints, then, destroy it
            if ( healthPoints <= 0 )
                Destroy( gameObject );
        }
    }
}
