using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance; 

    [SerializeField]
    private int _startBulletAmount;

    [SerializeField]
    private Bullets _bulletPrefab;

    private Queue<Bullets> _inactiveBullets = new Queue<Bullets>();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        StartSetup();
    }


    private void StartSetup() 
    {
        for(int i = 0; i < _startBulletAmount; i++)
        {
            CreateOneMoreBullet();
        }
    }

    private void CreateOneMoreBullet()
    {
        Bullets bullet = Instantiate(_bulletPrefab, this.transform);
        bullet.transform.position = this.transform.position;
        bullet.gameObject.SetActive(false);
        bullet._myManager = this;
        _inactiveBullets.Enqueue(bullet);
    }

    private void CreateMoreBullets(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            CreateOneMoreBullet();
        }
    }

    public void ReturnBullet(Bullets ret)
    {
        ret.gameObject.SetActive(false);
        ret.transform.position = this.transform.position;
        _inactiveBullets.Enqueue(ret);
    }

    public List<Bullets> GiveBullets(int amount)
    {
        List<Bullets> ret = new List<Bullets>();

        if(amount > _inactiveBullets.Count)
        {
            CreateMoreBullets(amount - _inactiveBullets.Count);
        }

        for(int i = 0; i < amount; i++)
        {
            ret.Add(_inactiveBullets.Dequeue());
        }

        return ret;
    }


}
