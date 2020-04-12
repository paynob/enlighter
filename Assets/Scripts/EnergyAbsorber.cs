using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnergyAbsorber : MonoBehaviour
{
    [SerializeField]
    [Range( 1, 20 )]
    private float energyToAbsorb;
    [SerializeField]
    private float energyAbsorbPerSecond;
    [SerializeField]
    private new Light light;

    private Collider coll;
    Energizer energy;

    public float Intensity { get { return light.intensity; } }

    private void Awake()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
    }

    private void OnTriggerEnter( Collider other )
    {
        var energizer = other.GetComponent<Energizer>();

        // If the collider is the player (player has Energizer component)
        if( energizer != null )
        {
            energy = energizer;
            // Stop current coroutines (maybe it's decreasing intensity...)
            StopAllCoroutines();
            // Start Increasing Intensity
            StartCoroutine( IncreaseIntensity() );
        }
    }

    private void OnTriggerExit( Collider other )
    {
        var energizer = other.GetComponent<Energizer>();
        // If the collider is the player (player has Energizer component)
        if( energizer != null )
        {
            // Stop current coroutines (maybe it's increasing intensity...)
            StopAllCoroutines();
            // Start Increasing Intensity
            StartCoroutine( DecreaseIntensity() );
        }
    }


    private IEnumerator DecreaseIntensity()
    {
        // Decrease intensity each frame until it is at value of 0
        while( light.intensity > 0 )
        {
            float amount = Time.deltaTime * energyAbsorbPerSecond;
            // Give energy back to the player
            energy.RecoverEnergy( amount );
            //Decrease light intensity
            light.intensity -= amount;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator IncreaseIntensity()
    {
        // Increase intensity each frame until it reaches total energy to absorb
        while( light.intensity < energyToAbsorb )
        {
            float amount = Time.deltaTime * energyAbsorbPerSecond;
            // Take energy from the player
            energy.GiveEnergy( amount );
            // Increase light intensity
            light.intensity += amount;
            yield return new WaitForEndOfFrame();
        }
    }
}
