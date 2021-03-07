using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public static SpaceShip instance;
    public Transform center;
    public float multipilier;
    public float speed = 4;
  
    public InputManager ınputManager;

    private void Awake()
    {
        instance = this;
    }
    public void Update()
    {
        Vector3 look = center.position - transform.position;
        float rot_z = (Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg)+90;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rot_z),0.05f);

        multipilier += ınputManager.result*speed*Time.deltaTime;

        float x = Mathf.Cos(multipilier);
        float y = Mathf.Sin(multipilier);


        transform.position = new Vector3(center.position.x + x, center.position.y+y, 0)*2;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="bonus")
        {
            Destroy(collision.gameObject);
            GameManager.instance.UpdateBonus(1);
        }
    }

}
