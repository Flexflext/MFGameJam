using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSides : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float kickRange;
    [SerializeField] private float kickStrenght;
    [SerializeField] private KeyCode leftKick;
    [SerializeField] private KeyCode rightKick;
    [SerializeField] private float timeBetweenKicks;

    private Animator _anim;
    private SpriteRenderer _spriteRend;

    private bool canKick = true;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _spriteRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(leftKick) && canKick)
        {
            Kick(2);
            Debug.Log("KickLeft"); 
            StartCoroutine(TimeBetweenKicks());
        }
        if (Input.GetKeyDown(rightKick) && canKick)
        {
            Kick(1);
            Debug.Log("KickRight");
            StartCoroutine(TimeBetweenKicks());
        }
    }

    private void Kick(int _idx)
    {
        Vector3 targetleft = new Vector3(-1, 0,0);
        Vector3 targetRight = new Vector3(1, 0 ,0);
        Vector3 target;
        RaycastHit2D hitInfo;

        switch (_idx)
        {
            case 1:
                target = targetRight;
                _spriteRend.flipX = false;
                break;
            case 2:
                target = targetleft;
                _spriteRend.flipX = true;
                break;
            default:
                target = targetleft;
                _spriteRend.flipX = true;
                break;
        }
        _anim.SetTrigger("IsAttacing");

        Vector3 direction = transform.position + target;

        hitInfo = Physics2D.Raycast(transform.position, direction - transform.position, kickRange, enemyLayer);

        if (hitInfo.collider != null)
        {
            Debug.Log(hitInfo.collider.tag);

            if (hitInfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("HuHu");
                Rigidbody2D rb = hitInfo.collider.GetComponent<Rigidbody2D>();
                rb.AddForce(target * kickStrenght);
            }
        }
    }

    IEnumerator TimeBetweenKicks()
    {
        yield return new WaitForSeconds(timeBetweenKicks);
        canKick = true;
    }

    private void OnDrawGizmosSelected()
    { 
        Gizmos.DrawWireSphere(transform.position, kickRange);
    }
}
