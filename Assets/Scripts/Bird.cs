using UnityEngine;

public class Bird : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    private GameController gameController;
    private new ParticleSystem particleSystem;

    [SerializeField]
    private float jumpHeigh;

    public AudioClip wing;
    public AudioClip die;
    public AudioClip hit;
    #endregion

    #region UnityEvents
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameController = gameController = GameController.GetGameController();
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameController.GameOver || gameController.GamePaused)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2();
            rb.AddForce(new Vector2(0, jumpHeigh), ForceMode2D.Impulse);

            particleSystem.Play();

            audioSource.PlayOneShot(wing, 0.05f);


            animator.SetTrigger("Fly");
        }

        else if (gameController.GameOnStart)
        {
            transform.position = new Vector3();
            rb.velocity = new Vector2();
            return;
        }

        var res = rb.velocity.y / 10 * 20;
        transform.rotation = Quaternion.Euler(0, 0, res);

        var pos = transform.position;
        pos.x = 0;

        transform.position = pos;
    }
    #endregion

    #region Other
    public void Kill()
    {
        gameController.GameOver = true;

        rb.velocity = new Vector2();

        rb.AddForce(new Vector2(-5, 5), ForceMode2D.Impulse);
        rb.angularVelocity = 250f;

        audioSource.PlayOneShot(die, 0.05f);
        audioSource.PlayOneShot(hit, 0.05f);

        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 10f);
    }
    #endregion
}
