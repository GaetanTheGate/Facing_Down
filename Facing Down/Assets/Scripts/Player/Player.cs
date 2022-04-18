using UnityEngine;

public class Player : AbstractPlayer
{
    public StatPlayer stat;
    private CameraManager camManager;
    public Entity self;
    public DirectionPointer pointer;
    public Camera gameCamera;
    public Inventory inventory;





    public override void Init()
    {
        stat = GetComponent<StatPlayer>();
        if (stat == null)
            stat = gameObject.AddComponent<StatPlayer>();


        inventory = self.GetComponent<Inventory>();
        if (inventory == null)
            inventory = self.gameObject.AddComponent<Inventory>();


        AbstractPlayer player;

        player = gameObject.GetComponent<PlayerDash>();
        if (player == null)
        {
            player = gameObject.AddComponent<PlayerDash>();
            player.Init();
        }

        player = gameObject.GetComponent<PlayerAttack>();
        if (player == null)
        {
            player = gameObject.AddComponent<PlayerAttack>();
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

        player = gameObject.GetComponent<PlayerCollisionStructure>();
        if (player == null)
        {
            player = gameObject.AddComponent<PlayerCollisionStructure>();
            player.Init();
        }
    }

}
