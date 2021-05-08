using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public bool PlayerIsStunned;

    [SerializeField] private float playerSpeed;

    private Vector3 moveDir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerIsStunned)
        {
            HandleInput();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * playerSpeed * Time.fixedDeltaTime;
        InGameUI.Instance.ChangeSpeedValue((rb.velocity.normalized).magnitude);
    }

    private void HandleInput()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        moveDir = new Vector2(xAxis, yAxis).normalized;
    }


}
