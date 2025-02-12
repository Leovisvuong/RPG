using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warning : Singleton<Warning>
{
    public TextMeshProUGUI warnText;

    [SerializeField] private float warnExistTime = 1;
    [SerializeField] private float fadeTime;

    [SerializeField] private bool isDoWarn = false;
    public void DoWarn(){
        if(isDoWarn) return;
        isDoWarn = true;
        StartCoroutine(FadeWarnRoutine(1));
    }

    private IEnumerator FadeWarnRoutine(float targetAlpha){
        while(!Mathf.Approximately(warnText.color.a,targetAlpha)){
            float alpha = Mathf.MoveTowards(warnText.color.a, targetAlpha, fadeTime * Time.deltaTime);
            warnText.color = new Color(warnText.color.r, warnText.color.g, warnText.color.b, alpha);
            yield return null;
        }
        Debug.Log(1);

        yield return new WaitForSeconds(warnExistTime);

        targetAlpha = 0;

        while(!Mathf.Approximately(warnText.color.a,targetAlpha)){
            float alpha = Mathf.MoveTowards(warnText.color.a, targetAlpha, fadeTime * Time.deltaTime);
            warnText.color = new Color(warnText.color.r, warnText.color.g, warnText.color.b, alpha);
            yield return null;
        }

        isDoWarn = false;
    }
}
