using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : TrapScript
{
    public float rotationSpeed = 1;
    public bool rotateToLeft;

    private void Update()
    {
        if(!rotateToLeft)
        transform.Rotate(0, 0, rotationSpeed * SlowMotionTime);
        else transform.Rotate(0, 0, -rotationSpeed * SlowMotionTime);
    }
}
