using UnityEngine;

public class Energizer : MonoBehaviour
{
    [SerializeField]
    private int maxEnergy = 100;

    [SerializeField]
    private float energyRecoverPerSecond = 5f;

    private float currentEnergy;

    private void Awake()
    {
        currentEnergy = maxEnergy;
    }

    private void Update()
    {
        RecoverEnergy( energyRecoverPerSecond * Time.deltaTime );
    }

    // Give energy to another entity
    public void GiveEnergy(float amount )
    {
        currentEnergy -= amount;

        if ( currentEnergy < 0 )
        {
            Time.timeScale = 0;
            FindObjectOfType<GameManager>().FinishGame();
        }
    }

    // Recover energy
    public void RecoverEnergy( float amount )
    {
        currentEnergy = Mathf.Clamp( currentEnergy + amount, 0, maxEnergy );
    }
}
