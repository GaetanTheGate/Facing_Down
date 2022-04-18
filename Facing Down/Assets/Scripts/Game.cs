using UnityEngine;

public class Game : MonoBehaviour
{
    public static GameController controller;
    public static TimeManager time;
    public static Player player;

    private void Start()
    {
        controller = gameObject.GetComponent<GameController>();
        if (controller == null)
            controller = gameObject.AddComponent<GameController>();

        time = gameObject.GetComponent<TimeManager>();
        if (time == null)
            time = gameObject.AddComponent<TimeManager>();

        player = gameObject.GetComponentInChildren<Player>();
    }

	private void Update() {
		if(Input.GetKeyDown(KeyCode.A)) {
            player.inventory.AddItem(new AttackMultUpItem());
		}
		if(Input.GetKeyDown(KeyCode.Z)) {
            player.inventory.AddItem(new AttackUpItem());
		}
		if(Input.GetKeyDown(KeyCode.E)) {
            player.inventory.AddItem(new PrintItem());
		}
        if(Input.GetKeyDown(KeyCode.Q)) {
            player.inventory.RemoveItem(new AttackMultUpItem());
		}
		if(Input.GetKeyDown(KeyCode.S)) {
            player.inventory.RemoveItem(new AttackUpItem());
		}
		if(Input.GetKeyDown(KeyCode.D)) {
            player.inventory.RemoveItem(new PrintItem());
		}
	}
}
