using UnityEngine;

public class FreezeManager : Singleton<FreezeManager>
{
    public bool gamePause = false;

    public void DoFreeze(){
        gamePause = true;
        Warning.Instance.FadeClearAll();
        Time.timeScale = 0;
    }

    public void StopFreeze(){
        gamePause = false;
        Time.timeScale = 1;
    }
}
