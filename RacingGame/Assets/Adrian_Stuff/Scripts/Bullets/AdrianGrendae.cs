using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdrianGrendae : GrenadeBehaviour
{
    [SerializeField]
    protected float _bulletSpeed;

    [SerializeField]
    protected int _rounds;

    [SerializeField]
    protected float _Intervall;

    [SerializeField]
    protected bool _spiral;
    private int currentBuletIndex;

    private Vector3[] _directions;

    private int _roundsShot = 0;
    private int _bulletsShot = 0;

    private void Awake()
    {
        _directions = Form.GetCircleDiretions(-Vector3.up, Vector3.forward, BulletAmount);
        foreach(Vector3 dir in _directions)
        {
            Debug.DrawLine(Vector3.zero, dir, Color.red, 10.0f);
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    [ContextMenu("Explode")]
    protected override void Expolde()
    {
        if (_spiral)
        {
            _bulletsShot = 0;
            _roundsShot = 0;
            StartCoroutine(ShootSpiral(ProjectileManager.Instance.GiveBullets(BulletAmount * _rounds)));
        }
        else
        {
            _bulletsShot = 0;
            _roundsShot = 0;
            StartCoroutine(ShootCircles(ProjectileManager.Instance.GiveBullets(BulletAmount * _rounds)));
        }
    }

    private IEnumerator ShootSpiral(List<Bullets> bullets)
    {

        
        while (_bulletsShot < BulletAmount * _rounds)
        {
            bullets[_bulletsShot].gameObject.SetActive(true);
            bullets[_bulletsShot].Spawn(this.transform.position, _directions[_bulletsShot % BulletAmount], _bulletSpeed);
            //Debug.Log(_bulletsShot % _rounds);
            _bulletsShot++;
            yield return new WaitForSeconds(_Intervall / BulletAmount);
        }

        base.Expolde();
    }

    private IEnumerator ShootCircles(List<Bullets> bullets)
    {
        while (_roundsShot < _rounds)
        {
            ShootCirle(bullets);
            _bulletsShot = 0;
            _roundsShot++;
            yield return new WaitForSeconds(_Intervall);
        }

        //Debug.Log("Done");
        base.Expolde();
    }
    private void ShootCirle(List<Bullets> bullets)
    {
        while (_bulletsShot < BulletAmount)
        {
            int id = _bulletsShot + (_roundsShot * BulletAmount);
            bullets[id].gameObject.SetActive(true);
            bullets[id].Spawn(this.transform.position, _directions[_bulletsShot], _bulletSpeed);
            //Debug.Log("shoot");
            _bulletsShot++;
        }
    }
}
