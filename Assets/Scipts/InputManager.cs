using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector3 firstpos;
  
    private float screen;
    public float Result
    {
        get
        {
            return result;
        }
    }
    public float result;


    public void Start()
    {
        screen = Screen.width / 2;
    }
    public void Update()
    {
        Inputmanage();
    }
    public void Inputmanage()
    {
      switch(Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
            case RuntimePlatform.Android:

                Touch[] touch = Input.touches;
                if(touch.Length>0)
                {
                    Touch touch1 = Input.GetTouch(0);
                    TouchPhase touchPhase = touch1.phase;
                    if(touchPhase==TouchPhase.Began)
                    {
                        firstpos = touch1.position;
                      

                    }
                    else if(touchPhase==TouchPhase.Moved)
                    {
                        firstpos = touch1.position;
                        if(firstpos.x>screen)
                        {
                            result =1;

                        }
                        else
                        {
                            result =-1;
                        }
                       

                    }
                    else if(touchPhase==TouchPhase.Ended)
                    {
                        result = 0;
                    }
                }

                break;
            case RuntimePlatform.WindowsEditor:
                if (Input.GetMouseButtonDown(0))
                {
                    firstpos = Input.mousePosition;


                }
                else if (Input.GetMouseButton(0))
                
                {
                    firstpos = Input.mousePosition;
                    if (firstpos.x > screen)
                    {
                        result = 1;

                    }
                    else
                    {
                        result = -1;
                    }


                }
                else if(Input.GetMouseButtonUp(0))
                {
                    result = 0;
                }

                break;
        }
    }
}
