using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] enemySpawnPoints;

    [SerializeField]
    private EnemyAI enemyPrefab;


    [SerializeField]
    private Text time;
    [SerializeField]
    private GameObject restartButton;

    private float startTime;

    private void Awake()
    {
        startTime = Time.time;
    }

    private IEnumerator Start()
    {
        float spawnTime = 7f;

        // Spawn enemies forever
        while( true )
        {
            // Spawn it at random position within spawn positions
            EnemyAI enemy = Instantiate( enemyPrefab, enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position, Quaternion.identity );
            yield return new WaitForSeconds( spawnTime );
            // Increase the frequency at which enemies will spawn
            spawnTime -= 0.1f;
        }
    }

    private void Update()
    {
        //  Show the time the player is alive
        time.text = (Time.time - startTime).ToString("#0.000");
    }

    public void FinishGame()
    {
        // When the player dies, it will call this function to show the restart button
        restartButton.SetActive( true );
    }

    public void Restart()
    {
        // Restore time scale
        Time.timeScale = 1f;
        // And load again the scene
        UnityEngine.SceneManagement.SceneManager.LoadScene( 0 );
    }
}
