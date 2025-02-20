using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenTransition = 5f;
    [SerializeField] private List<TextMeshProUGUI> storyText = new List<TextMeshProUGUI>();
    [SerializeField] private Button skipButton;
    [SerializeField] private float skipCoolDown = 1f;

    private int i = 0;
    public bool canSkip = true;

    private void Start(){
        FadeToBlackText();
        skipButton.onClick.AddListener(Skip);
    }

    private void FadeToBlackText(){
        if(i < storyText.Count){
            StartCoroutine(FadeRoutine(1));
        }
        else{
            GameObject exit = GameObject.Find("AreaExit");
            exit.GetComponent<AreaExit>().LoadScene();
        }
    }

    private void FadeToClearText(){
        StartCoroutine(FadeRoutine(0));
    }

    private void Skip(){
        if(!canSkip) return;

        canSkip = false;
        StopAllCoroutines();
        storyText[i].color = new Color(storyText[i].color.r, storyText[i].color.g, storyText[i].color.b, 0);
        i++;
        FadeToBlackText();
        StartCoroutine(CountdownSkipCoolDown());
    }

    private IEnumerator CountdownSkipCoolDown(){
        yield return new WaitForSeconds(skipCoolDown);
        canSkip = true;
    }

    private IEnumerator FadeRoutine(float targetAlpha){
        while(!Mathf.Approximately(storyText[i].color.a,targetAlpha)){
            float alpha = Mathf.MoveTowards(storyText[i].color.a, targetAlpha, Time.deltaTime);
            storyText[i].color = new Color(storyText[i].color.r, storyText[i].color.g, storyText[i].color.b, alpha);
            yield return null;
        }
        if(targetAlpha > 0){
            yield return new WaitForSeconds(timeBetweenTransition);
            FadeToClearText();
        }
        else{
            i++;
            FadeToBlackText();
        }
    }
}
