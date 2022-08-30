using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    #region Variables
    private GameController gameController;

    public GameObject wall;
    public float Timer;
    public Vector3 spawnPos;
    #endregion

    #region UnityEvents
    private void Awake()
    {
        gameController = GameController.GetGameController();
    }

    private void Update()
    {
        if (gameController.GameOver ||
            gameController.GamePaused ||
            gameController.GameOnStart)
        {
            return;
        }

        Timer += Time.deltaTime;

        if (Timer > gameController.TimeBetweenWalls)
        {
            Instantiate(wall, spawnPos, new Quaternion(), transform);
            Timer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnPos, 0.2f);
    }
    #endregion
}
