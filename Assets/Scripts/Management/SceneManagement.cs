public class SceneManagement : Singleton<SceneManagement>
{
    public string sceneTransitionName;

    public void SetTransitionName(string sceneTransitionName){
        this.sceneTransitionName = sceneTransitionName;
    }
}
