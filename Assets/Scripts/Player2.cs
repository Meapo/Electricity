using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerController
{

    protected override void isPressedJump()
    {
        base.isPressedJump();
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isPressed = true;
        }
    }

    protected override void pressMove()
    {
        base.pressMove();
        h = Input.GetAxisRaw("Player2Horizontal");
        if (h > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (h < 0 && isFacingRight)
        {
            Flip();
        }
    }
}
