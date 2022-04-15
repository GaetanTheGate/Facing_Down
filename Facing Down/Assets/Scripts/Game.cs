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

        Item atkItem = new AttackUpItem();
        Item atkMultItem = new AttackMultUpItem();
        player.inventory.AddItem(atkItem);
        Debug.LogAssertion(player.stat.getAtk() == 110);
        player.inventory.AddItem(atkMultItem);
        Debug.LogAssertion(player.stat.getAtk() == 121);
        player.inventory.RemoveItem(atkItem);
        Debug.LogAssertion(player.stat.getAtk() == 110);
        player.inventory.RemoveItem(atkMultItem);
        Debug.LogAssertion(player.stat.getAtk() == 100);
    }
}
