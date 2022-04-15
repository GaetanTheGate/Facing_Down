using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameController controller;
    public static TimeManager time;

    private void Start()
    {
        controller = gameObject.GetComponent<GameController>();
        if (controller == null)
            controller = gameObject.AddComponent<GameController>();

        time = gameObject.GetComponent<TimeManager>();
        if (time == null)
            time = gameObject.AddComponent<TimeManager>();
    }
}
