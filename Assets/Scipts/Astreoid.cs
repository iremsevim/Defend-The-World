using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Astreoid : MonoBehaviour
{
    public static List<Astreoid> allAstroid = new List<Astreoid>();
    public ParticleSystem fireexplParticle;
    public float speed;

    private void Awake()
    {
        allAstroid.Add(this);
    }
    private void OnDestroy()
    {
        allAstroid.Remove(this);
    }
    public void ThrowAstroid(float _speed,Vector3 target)
    {
      
        speed = _speed;
        transform.DOMove(target, _speed).OnComplete(() => 
        {
          
            Destroy(gameObject);        
        });
    }
    public void Destroy()
    {
        fireexplParticle.Play();
        fireexplParticle.transform.SetParent(null);
        Destroy(gameObject);
    }
   
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<SpaceShip>())
        {
          
            Camera.main.transform.DOShakePosition(0.25f);
            Destroy();
            
        }
        else if (collision.gameObject.tag == "World")
        {
            GameManager.instance.worldExplosion.Play();
            GameManager.instance.worldExplosion.transform.SetParent(null);
            collision.gameObject.SetActive(false);
            GameManager.instance.GameOver();
            Destroy(gameObject);
            Destroy(GameManager.instance.worldExplosion.gameObject, 1f);

        }
    }
}
