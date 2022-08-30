using UnityEngine;

public class Walls : MonoBehaviour
{
    #region Variables
    private GameObject player;
    private AudioSource audioSource;
    private GameController gameController;

    private bool passed;

    public AudioClip point;
    #endregion

    #region UnityEvents
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = gameController = GameController.GetGameController();
        audioSource = GetComponent<AudioSource>();

        var pos = transform.position;
        pos.y += Random.Range(-1.5f, 3f);

        transform.position = pos;
    }


    private void Update()
    {
        if (gameController.GameOver || gameController.GamePaused)
        {
            return;
        }

        transform.Translate(new Vector2(-gameController.BackSpeed * Time.deltaTime, 0));

        if (player != null && transform.position.x < player.transform.position.x && !passed)
        {
            gameController.CurrentScore++;
            audioSource.PlayOneShot(point, 0.05f);
            passed = true;
        }

        if (transform.position.x < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Bird>().Kill();
    }
    #endregion
}
