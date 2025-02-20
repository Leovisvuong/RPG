using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
 
    private void Start(){
        if(transitionName == SceneManagement.Instance.sceneTransitionName){
            if(transitionName == "Enter"){
                UIFade.Instance.FadeImidiately(1);
            }
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }
}
