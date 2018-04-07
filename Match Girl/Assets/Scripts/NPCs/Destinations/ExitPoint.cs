using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoint : CrowdPoint {

    public override Vector3 GetPosition()
    {
        return transform.position;
    }
}
