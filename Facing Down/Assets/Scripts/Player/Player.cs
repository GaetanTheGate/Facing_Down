using UnityEngine;

public class Player : AbstractPlayer
{

    private StatEntity stat;
    public Entity self;
    public DirectionPointer pointer;
    public Camera gameCamera;
    public Inventory inventory;




    private bool attackPressed = false;

    public override void Init()
    {
        stat = self.GetComponent<StatEntity>();
        if (stat == null)
            stat = self.gameObject.AddComponent<StatEntity>();


        inventory = self.GetComponent<Inventory>();
        if (inventory == null)
            inventory = self.gameObject.AddComponent<Inventory>();

<<<<<<< Updated upstream
        self.GetComponent<BoxCollider2D>().sharedMaterial = material;
    }
=======
        AbstractPlayer player;
>>>>>>> Stashed changes

        player = gameObject.GetComponent<PlayerDash>();
        if(player == null)
        {
            player = gameObject.AddComponent<PlayerDash>();
            player.Init();
        }

        player = gameObject.GetComponent<PlayerBulletTime>();
        if (player == null)
        {
            player = gameObject.AddComponent<PlayerBulletTime>();
            player.Init();
        }

        player = gameObject.GetComponent<PlayerMaterial>();
        if (player == null)
        {
            player = gameObject.AddComponent<PlayerMaterial>();
            player.Init();
        }
    }

}
