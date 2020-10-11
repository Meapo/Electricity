using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerController
{

    protected override void isPressedJump()
    {
        base.isPressedJump();
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
        {
            isPressed = true;
        }
        
    }

    protected override void pressMove()
    {
        base.pressMove();
        h = Input.GetAxisRaw("Player2Horizontal");
        if (h > 0 && StopRight)
        {
            h = 0;
        }
        else if (h < 0 && StopLeft)
        {
            h = 0;
        }
        if (h > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (h < 0 && isFacingRight)
        {
            Flip();
        }
    }

    protected override void pressPickup()
    {
        base.pressPickup();
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (pickupItem != null)
            {
                drop();
            }
            else
            {
                pickup();
            }
        }
    }
}
