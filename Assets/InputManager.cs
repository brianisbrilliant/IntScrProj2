using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Gun gun;             // drag the gun to this variable in the inspector
                                // should this route through the playercontroller? nah..... maybe!

    public bool debug = false;

    void Start() {
        if(debug) Debug.Log("Input Manager is starting up.");
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Mouse.current;
        if(mouse == null) return;

        // isPressed
        if(mouse.leftButton.isPressed) {
            if(debug) Debug.Log("Left Mouse Button was pressed this frame.");
            
            if(gun != null) {
                gun.Fire();         // calling the fire method on the gun variable.
            } else {
                if(debug) Debug.Log("There is no gun to fire!");
            }
        }

        var keyboard = Keyboard.current;
        if(keyboard == null) return;

        if(keyboard.rKey.wasPressedThisFrame) {
            if(gun != null) {
                gun.Reload();
            }
        }
    }
}
