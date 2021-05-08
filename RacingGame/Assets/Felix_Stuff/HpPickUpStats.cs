using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPickUpStats : MonoBehaviour
{
    [SerializeField] private float health;
    public float Health => health;

    [SerializeField] private float speed;

    private void Update()
    {
        transform.position -= new Vector3(0, 1, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndBorder"))
        {
            Debug.Log("Hier!");
            Destroy(this.gameObject);
        }
    }
}
