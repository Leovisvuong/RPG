using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeManager : Singleton<FreezeManager>
{
    public bool gamePause = false;

    public void DoFreeze(){
        gamePause = true;
        Time.timeScale = 0;
    }

    public void StopFreeze(){
        gamePause = false;
        Time.timeScale = 1;
    }
}
