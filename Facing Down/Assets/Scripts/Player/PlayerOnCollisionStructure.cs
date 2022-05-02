using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnCollisionStructure : AbstractPlayer
{
    private StatPlayer statPlayer;
    private EntityCollisionStructure entityCollisionStructure;

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
        onStayCollide();
    }

    public void onStayCollide()
    {
        if (entityCollisionStructure.isEnteringGround || entityCollisionStructure.isEnteringCeiling || entityCollisionStructure.isEnteringWall)
        {
            Game.player.inventory.OnGroundCollisionEnter();
        }

        //comportement pour toute la durée de la collision avec le sol
        if (entityCollisionStructure.isGrounded && !entityCollisionStructure.isEnteringGround)
        {
            statPlayer.numberOfDashes = 0;
        }
        //comportement au moment de la collision avec le sol
        else if (entityCollisionStructure.isGrounded && entityCollisionStructure.isEnteringGround)
        {

        }

        //comportement pour toute la durée de la collision avec le mur
        if (entityCollisionStructure.isWalled && !entityCollisionStructure.isEnteringWall)
        {

        }
        //comportement au moment de la collision avec le mur
        else if (entityCollisionStructure.isWalled && entityCollisionStructure.isEnteringWall)
        {

        }

        //comportement pour toute la durée de la collision avec le plafond
        if (entityCollisionStructure.isCeilinged && !entityCollisionStructure.isEnteringCeiling)
        {

        }
        //comportement au moment de la collision avec le plafond
        else if (entityCollisionStructure.isCeilinged && entityCollisionStructure.isEnteringCeiling)
        {

        }
    }
}
