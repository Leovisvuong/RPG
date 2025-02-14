using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class Warning : Singleton<Warning>
{
    [SerializeField] private GameObject warnTextPrefab;
    [SerializeField] private Stack<GameObject> warnTextObj;

    [SerializeField] private float warnExistTime = 1;
    [SerializeField] private float fadeTime;

    private List<TextMeshProUGUI> textIsUsing;

    protected override void Awake()
    {
        base.Awake();

        warnTextObj = new Stack<GameObject>();
        textIsUsing = new List<TextMeshProUGUI>();
    }

    public void DoWarn(string warnText, Color textColor){
        TextMeshProUGUI warnTextUse;

        if(warnTextObj.Count == 0){
            GameObject instance = Instantiate(warnTextPrefab);
            instance.transform.SetParent(gameObject.transform, false);
            warnTextUse = instance.GetComponent<TextMeshProUGUI>();
        }
        else{
            warnTextUse = warnTextObj.Pop().GetComponent<TextMeshProUGUI>();
        }

        warnTextUse.text = warnText;
        warnTextUse.color = textColor;
        textIsUsing.Add(warnTextUse);
        StartCoroutine(FadeWarnRoutine(1, warnTextUse));
    }

    private IEnumerator FadeWarnRoutine(float targetAlpha, TextMeshProUGUI targetText){
        while(!Mathf.Approximately(targetText.color.a,targetAlpha)){
            float alpha = Mathf.MoveTowards(targetText.color.a, targetAlpha, fadeTime * Time.deltaTime);
            targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, alpha);
            yield return null;
        }
        Debug.Log(1);

        yield return new WaitForSeconds(warnExistTime);

        targetAlpha = 0;

        while(!Mathf.Approximately(targetText.color.a,targetAlpha)){
            float alpha = Mathf.MoveTowards(targetText.color.a, targetAlpha, fadeTime * Time.deltaTime);
            targetText.color = new Color(targetText.color.r, targetText.color.g, targetText.color.b, alpha);
            yield return null;
        }

        textIsUsing.Remove(targetText);
        warnTextObj.Push(targetText.gameObject);
    }

    public void FadeClearAll(){
        foreach(var i in textIsUsing){
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        }
        if(textIsUsing.Count > 0) textIsUsing.RemoveRange(0, textIsUsing.Count - 1);
    }
}
