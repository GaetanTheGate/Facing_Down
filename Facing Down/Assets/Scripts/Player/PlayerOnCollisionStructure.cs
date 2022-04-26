using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnCollisionStructure : AbstractPlayer
{
    private StatPlayer statPlayer;
    private EntityCollisionStructure entityCollisionStructure;
    private bool isEnteringGround = false;
    public override void Init()
    {

        statPlayer = gameObject.GetComponent<Player>().stat;
        entityCollisionStructure = gameObject.GetComponent<Player>().self.GetComponent<EntityCollisionStructure>();
        if(entityCollisionStructure == null)
        {
            entityCollisionStructure = gameObject.GetComponent<Player>().self.gameObject.AddComponent<EntityCollisionStructure>();
            entityCollisionStructure.Init();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //comportement pour toute la durée de la collision avec le sol
        if (entityCollisionStructure.isGrounded && !isEnteringGround)
        {
            statPlayer.numberOfDashes = 0;
        }
        //comportement au moment de la collision avec le sol
        else if (entityCollisionStructure.isGrounded && isEnteringGround)
        {
            
        }

        //comportement pour toute la durée de la collision avec le mur
        if (entityCollisionStructure.isGrounded && !isEnteringGround)
        {
            
        }
        //comportement au moment de la collision avec le mur
        else if (entityCollisionStructure.isGrounded && isEnteringGround)
        {

        }

        //comportement pour toute la durée de la collision avec le plafond
        if (entityCollisionStructure.isGrounded && !isEnteringGround)
        {
            
        }
        //comportement au moment de la collision avec le plafond
        else if (entityCollisionStructure.isGrounded && isEnteringGround)
        {

        }
    }
}
