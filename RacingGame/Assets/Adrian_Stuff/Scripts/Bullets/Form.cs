using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Form
{
    public static Vector3[] GetCircleDiretions(Vector3 startDirection, Vector3 rotationAxis  ,int directionAmounts)
    {
        Vector3[] ret = new Vector3[directionAmounts];
        float angle = 360 / directionAmounts;
        Quaternion rot = new Quaternion();
        rot.eulerAngles = rotationAxis * angle;
        Matrix4x4 rotation = Matrix4x4.Rotate(rot);

        Vector3 start = startDirection;

        for(int i = 0; i < directionAmounts; i++)
        {
            ret[i] = start;
            start = rotation * start;
        }

        return ret;
    }
}
