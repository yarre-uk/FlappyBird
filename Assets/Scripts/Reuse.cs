using UnityEngine;

public class Reuse : MonoBehaviour
{
    #region Variables
    private GameController gameController;

    private float speed;

    public string Type;
    public float Min;
    public float Max;
    #endregion

    #region UnityEvents
    private void Awake()
    {
        gameController = GameController.GetGameController();
    }

    private void Update()
    {
        if (gameController.GameOver || gameController.GamePaused || gameController.GameOnStart)
        {
            return;
        }

        speed = (Type == "Walls") ? gameController.WallsSpeed : gameController.BackSpeed;

        if (transform.position.x < Min)
        {
            var pos = transform.position;
            pos.x = Max + (Max + transform.position.x);

            transform.position = pos;
        }

        transform.Translate(speed * Time.deltaTime * Vector2.left);
    }
    #endregion
}
