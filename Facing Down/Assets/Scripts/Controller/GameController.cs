using UnityEngine;

public class GameController : MonoBehaviour
{


    [Range(0.0f, 2.0f)] public float sensibility = 0.8f;

    private bool isMovementPressed = false;
    private bool isMovementHeld = false;
    private bool isMovementReleased = false;
    private bool isAttackPressed = false;
    private bool isAttackHeld = false;
    private bool isAttackReleased = false;
    private bool isBulletTimePressed = false;
    private bool isBulletTimeHeld = false;
    private bool isBulletTimeReleased = false;

    private Vector2 pointer = new Vector2(0.0f, 0.0f);

    public bool lowSensitivity = false;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputePress();
        ComputeHeld();
        ComputeReleased();


        pointer.x = Input.GetAxis("Mouse X") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
        pointer.y = Input.GetAxis("Mouse Y") * Game.controller.sensibility * (lowSensitivity ? 0.5f : 1.0f);
    }

    private void ComputePress()
    {
        //key dash
        if (Input.GetKeyDown(Options.Get().dicoCommand["dash"]))
            isMovementPressed = true;
        else
            isMovementPressed = false;

        //key attack
        if (Input.GetKeyDown(Options.Get().dicoCommand["attack"]))
            isAttackPressed = true;
        else
            isAttackPressed = false;

        //key bullet time
        if (Input.GetKeyDown(Options.Get().dicoCommand["bulletTime"]))
            isBulletTimePressed = true;
        else
            isBulletTimePressed = false;
    }

    private void ComputeHeld()
    {
        if (Input.GetKey(Options.Get().dicoCommand["dash"]))
            isMovementHeld = true;
        else
            isMovementHeld = false;

        if (Input.GetKey(Options.Get().dicoCommand["attack"]))
            isAttackHeld = true;
        else
            isAttackHeld = false;

        if (Input.GetKey(Options.Get().dicoCommand["bulletTime"]))
            isBulletTimeHeld = true;
        else
            isBulletTimeHeld = false;
    }

    private void ComputeReleased()
    {
        if (Input.GetKeyUp(Options.Get().dicoCommand["dash"]))
            isMovementReleased = true;
        else
            isMovementReleased = false;

        if (Input.GetKeyUp(Options.Get().dicoCommand["attack"]))
            isAttackReleased = true;
        else
            isAttackReleased = false;

        if (Input.GetKeyUp(Options.Get().dicoCommand["bulletTime"]))
            isBulletTimeReleased = true;
        else
            isBulletTimeReleased = false;
    }

    public bool IsMovementPressed()
    {
        return isMovementPressed;
    }
    public bool IsAttackPressed()
    {
        return isAttackPressed;
    }
    public bool IsBulletTimePressed()
    {
        return isBulletTimePressed;
    }

    public bool IsMovementHeld()
    {
        return isMovementHeld;
    }
    public bool IsAttackHeld()
    {
        return isAttackHeld;
    }
    public bool IsBulletTimeHeld()
    {
        return isBulletTimeHeld;
    }


    public bool IsMovementReleased()
    {
        return isMovementReleased;
    }
    public bool IsAttackReleased()
    {
        return isAttackReleased;
    }
    public bool IsBulletTimeReleased()
    {
        return isBulletTimeReleased;
    }

    public Vector2 getPointer()
    {
        return pointer;
    }
}
