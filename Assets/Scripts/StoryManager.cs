using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenTransition = 6f;
    [SerializeField] private List<Image> storyTextImage = new List<Image>();
    [SerializeField] private Button skipButton;
    [SerializeField] private float skipCoolDown = 1f;

    public int i = 0;
    private bool canSkip = true;

    private void Start(){
        FadeToBlackNextText();
        skipButton.onClick.AddListener(Skip);
    }

    private void FadeToBlackNextText(){
        if(i < storyTextImage.Count){
            UIFade.Instance.fadeScreen = storyTextImage[i];
            i++;
            StartCoroutine(CountdownFadeToBlack());
        }
        else{
            UIFade.Instance.FadeImidiately(0);
            GameObject exit = GameObject.Find("AreaExit");
            exit.GetComponent<AreaExit>().LoadScene();
        }
    }

    private void Skip(){
        if(!canSkip) return;

        canSkip = false;
        StopAllCoroutines();
        UIFade.Instance.FadeImidiately(0);
        FadeToBlackNextText();
        StartCoroutine(CountdownSkipCoolDown());
    }

    private IEnumerator CountdownFadeToBlack(){
        if(i==1) yield return new WaitForSeconds(1.5f);

        UIFade.Instance.FadeToBlack();
        yield return new WaitForSeconds(timeBetweenTransition);
        StartCoroutine(CountdownFadeToClear());
    }

    private IEnumerator CountdownFadeToClear(){
        UIFade.Instance.FadeToClear();
        yield return new WaitForSeconds(1);
        FadeToBlackNextText();
    }

    private IEnumerator CountdownSkipCoolDown(){
        yield return new WaitForSeconds(skipCoolDown);
        canSkip = true;
    }
}
