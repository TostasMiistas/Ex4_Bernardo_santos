using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> bullets = new List<GameObject>();
    public GameObject bullet;
    public int bulletNumber;
    public float timeUntilNextBullet= 3f;

    private void Start()
    {
        GameObject tempBullet;

        for (int i = 0; i < bulletNumber; i++)
        {
            tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            bullets.Add(tempBullet);
        }
        StartCoroutine(BulletDrop());
    }
            
        private IEnumerator BulletDrop()
        {
            while(true)
            {
                if(GetIdleBullet() != null)
                {
                    GetIdleBullet().transform.position = transform.position;
                    GetIdleBullet().SetActive(true);
                    yield return new WaitForSeconds(timeUntilNextBullet);
                }
                yield return null;
            }
        }
    private GameObject GetIdleBullet()
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }
        return null;
    }
    
}
