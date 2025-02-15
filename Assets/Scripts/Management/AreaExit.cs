using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private Button startButton;

    private float waitToLoadTime = 1f;

    private void Start(){
        if(startButton){
            startButton.onClick.AddListener(LoadScene);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<PlayerController>()){
            LoadScene();
        }
    } 

    public void LoadScene(){
        SceneManagement.Instance.SetTransitionName(sceneTransitionName);
        if(GetComponent<BoxCollider2D>()){

            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
        else SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator LoadSceneRoutine(){
        while(waitToLoadTime >=0){
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
