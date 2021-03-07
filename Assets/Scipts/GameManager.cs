using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject astroidprefab;
    public GameObject bonusPrefab;
    public RunTime runTime;
    public List<Transform> allastroid;
    public Transform world;
    public float rate = 0.5f;
    public float timer = 2f;
    public UnityEngine.UI.Text bonustext;
    public ParticleSystem worldExplosion;
    public float lastbonusTime;
   
        

    public void Awake()
    {
        instance = this;
      
    }
    public IEnumerator ReStartGame()
    {
        yield return new WaitForSeconds(2f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
       
            Bonus();
        world.Rotate(Vector3.forward * 180 * Time.deltaTime);
        if (!runTime.isgameStarted) return;
        if(Time.time-timer>rate)
        {
            timer = Time.time;
            CreateAstroid();
        }
    }
    public Vector2 FindBonusPoint()
    {
        Vector3 result = Vector2.zero;
        float multiper = SpaceShip.instance.multipilier;
     
        multiper += Random.Range(0.5f,(Mathf.PI * 2)-2f);

        float x = Mathf.Cos(multiper);
        float y = Mathf.Sin(multiper);

        result = new Vector3(world.position.x + x, world.position.y + y, 0) * 2;

        return result;
    }
  
    public void GameOver()
    {
        runTime.isgameStarted = false;
        Astreoid.allAstroid.ForEach(x => x.Destroy());
        StartCoroutine(ReStartGame());
    }
    

    public void CreateAstroid()
    {
     
        Transform rand = allastroid[Random.Range(0,allastroid.Count)];

        GameObject created = Instantiate(astroidprefab, rand.position, Quaternion.identity);

        created.GetComponent<Astreoid>().ThrowAstroid(runTime.currentspeed, world.position);
    
    }
    private void Bonus()
    {
        if(Time.time-lastbonusTime>4f)
        {
            lastbonusTime = Time.time;
            GameObject createdbonus = Instantiate(bonusPrefab, FindBonusPoint(), Quaternion.identity);
        }
      
    }
    public void UpdateBonus(int bonus)
    {
        runTime.currentBonus += bonus;
      
        bonustext.text = runTime.currentBonus.ToString();
    }
    public void MoonUsed()
    {
        if (runTime.currentBonus <= 0) return;
        Astreoid.allAstroid.ForEach(x =>x.Destroy());
      
        UpdateBonus(-1);
    }
    [System.Serializable]
    public class RunTime
    {
        public float currentspeed;
        public bool isgameStarted;
        public int currentBonus;
    }
}
