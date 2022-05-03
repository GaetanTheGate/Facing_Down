using UnityEngine;

public class Game : MonoBehaviour
{
    public static System.Random random = new System.Random();
    public static GameController controller;
    public static TimeManager time;
    public static Player player;

    private string playerPath = "Prefabs/Player/Player";
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
        if(player == null)
        {
            player = Instantiate(Resources.Load(playerPath, typeof(GameObject)) as GameObject).GetComponent<Player>();
            player.transform.SetParent(transform);
        }
    }
}
