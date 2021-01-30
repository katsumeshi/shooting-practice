using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour {
   public FixedJoystick joystick;
   public int move = 0;  

   void Update() {
       move = 0;
       if (joystick.Horizontal < 0) {
          move = -1;
       } else if (joystick.Horizontal > 0) {
          move = 1;
       }
   }
}