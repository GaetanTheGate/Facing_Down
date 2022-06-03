using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnCollisionStructure : AbstractPlayer
{
    private StatPlayer statPlayer;
    private EntityCollisionStructure entityCollisionStructure;

    private bool collidedLastFrame = false;

    protected override void Initialize()
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

        if (collidedLastFrame && !entityCollisionStructure.isGrounded && !entityCollisionStructure.isWalled && !entityCollisionStructure.isCeilinged) {
            Game.player.inventory.OnGroundCollisionLeave();
            collidedLastFrame = false;
        }

        //comportement pour toute la durée de la collision avec le sol
        if (entityCollisionStructure.isGrounded && !entityCollisionStructure.isEnteringGround)
        {
            statPlayer.ResetDashes();
            collidedLastFrame = true;
        }
        //comportement au moment de la collision avec le sol
        else if (entityCollisionStructure.isGrounded && entityCollisionStructure.isEnteringGround)
        {

        }
        

        //comportement pour toute la durée de la collision avec le mur
        if (entityCollisionStructure.isWalled && !entityCollisionStructure.isEnteringWall)
        {
            collidedLastFrame = true;
        }
        //comportement au moment de la collision avec le mur
        else if (entityCollisionStructure.isWalled && entityCollisionStructure.isEnteringWall)
        {

        }

        //comportement pour toute la durée de la collision avec le plafond
        if (entityCollisionStructure.isCeilinged && !entityCollisionStructure.isEnteringCeiling)
        {
            collidedLastFrame = true;
        }
        //comportement au moment de la collision avec le plafond
        else if (entityCollisionStructure.isCeilinged && entityCollisionStructure.isEnteringCeiling)
        {

        }
    }
}
