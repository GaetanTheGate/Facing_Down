using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameController controller;
    public static TimeManager time;
    public static Player player;

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(10, 12, true);
        controller = gameObject.GetComponent<GameController>();
        if (controller == null)
            controller = gameObject.AddComponent<GameController>();

        time = gameObject.GetComponent<TimeManager>();
        if (time == null)
            time = gameObject.AddComponent<TimeManager>();

        player = gameObject.GetComponentInChildren<Player>();
    }
}
