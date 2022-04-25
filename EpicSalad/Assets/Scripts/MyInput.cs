using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines all buttons for two player keyboard controls
//player a, player b

public static class MyInput
{
    //Store player control schemes in matching arrays
    //index = right, left, up, down, select
    static KeyCode[] keyCodes_a = {KeyCode.RightArrow,
    KeyCode.LeftArrow,
    KeyCode.UpArrow,
    KeyCode.DownArrow,
    KeyCode.Return };

    static KeyCode[] keyCodes_b = {KeyCode.D,
    KeyCode.A,
    KeyCode.W,
    KeyCode.S,
    KeyCode.LeftShift };


    //GetMovementVector called by player class to read in movement direction
    //input boolean defines which control set to read from

    public static Vector2 GetMovementVector(bool playerA) {
        KeyCode[] keys = (playerA) ? keyCodes_a : keyCodes_b;
        Vector2 move = Vector2.zero;
        float x = 0f;
        float y = 0f;
        if (Input.GetKey(keys[0])) x = 1f;
        else if (Input.GetKey(keys[01])) x = -1f;
        if (Input.GetKey(keys[2])) y = 1f;
        else if(Input.GetKey(keys[3])) y = -1f;
        move = new Vector2(x, y);
        return move;
    }


    //when the player presses the "use" key
    public static bool SelectPressed(bool playerA) {
        KeyCode[] keys = (playerA) ? keyCodes_a : keyCodes_b;
        return (Input.GetKeyDown(keys[4]));
    }
}
