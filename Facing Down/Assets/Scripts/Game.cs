using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour
{
    public static CoroutineStarter coroutineStarter;
    public static System.Random random = new System.Random();
    public static GameController controller;
    public static TimeManager time;
    public static Player player;

    public static RoomHandler currentRoom;

    private string playerPath = "Prefabs/Player/Player";
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i <= 31; i++)
        {
            Physics2D.IgnoreLayerCollision(13, i, true);
        }
        Physics2D.IgnoreLayerCollision(13, 11, false);
        controller = gameObject.GetComponent<GameController>();
        if (controller == null)
            controller = gameObject.AddComponent<GameController>();
        controller.Init();

        time = gameObject.GetComponent<TimeManager>();
        if (time == null)
            time = gameObject.AddComponent<TimeManager>();

        player = gameObject.GetComponentInChildren<Player>();
        if(player == null)
        {
            player = Instantiate(Resources.Load(playerPath, typeof(GameObject)) as GameObject).GetComponent<Player>();
            player.transform.SetParent(transform);
        }

        coroutineStarter = gameObject.GetComponent<CoroutineStarter>();
        if (coroutineStarter == null)
            coroutineStarter = gameObject.AddComponent<CoroutineStarter>();
    }

    private void FixedUpdate()
    {
        if(Random.Range(0, 1000) == 0)
        {
            SpriteRenderer[] allSprite = FindObjectsOfType<SpriteRenderer>();
            SpriteRenderer chosenSprite = allSprite[Random.Range(0, allSprite.Length)];

            Sprite sprite;
            if (Random.Range(0, 2) == 1)
                sprite = Resources.Load<Sprite>("Sprites/Enemies/lugi (very beau)");
            else
                sprite = Resources.Load<Sprite>("Sprites/Enemies/maro (very beau)");

            GameObject gameObject = new GameObject(":D");
            gameObject.AddComponent<SpriteRenderer>().sprite = sprite;
            gameObject.transform.position = chosenSprite.transform.position - new Vector3(0, 0, 0.1f);
            gameObject.transform.localScale = chosenSprite.transform.localScale;
            gameObject.transform.rotation = chosenSprite.transform.rotation;

            gameObject.transform.SetParent(chosenSprite.transform);

            Destroy(gameObject, 0.5f);
        }
    }
}
