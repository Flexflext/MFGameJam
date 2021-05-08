using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormTester : MonoBehaviour
{
    [SerializeField]
    private int amount; 

    private void Awake()
    {
        Vector3[] dir = Form.GetCircleDiretions(-this.transform.up, this.transform.forward, amount);

        foreach(Vector3 direction in dir)
        {
            Debug.DrawLine(Vector3.zero, direction, Color.red, 10.0f);
            Debug.Log(direction);
        }
    }
}
