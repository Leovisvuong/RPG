using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TransparentDetection : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] private float transparencyAmount = 0.6f;
    [SerializeField] private float fadeTime = 0.4f;
    [SerializeField] private List<Image> images;


    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    public void DoFadeClear(){
        if(Time.timeScale < 1) return;
        foreach(var i in images){
            if(i != null) StartCoroutine(FadeRoutine(i, fadeTime, i.color.a, transparencyAmount));
        }
    }

    public void DoFadeBlack(){
        foreach(var i in images){
            if(i != null) StartCoroutine(FadeRoutine(i, fadeTime, i.color.a, 1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<PlayerController>()){
            if(spriteRenderer){
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
            }
            else if(tilemap){
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.GetComponent<PlayerController>()){
            if(spriteRenderer){
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 1f));
            }
            else if(tilemap){
                StartCoroutine(FadeRoutine(tilemap, fadeTime, tilemap.color.a, 1f));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency){
        float elapsedTime = 0;
        while(elapsedTime < fadeTime){
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue,targetTransparency,elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetTransparency){
        float elapsedTime = 0;
        while(elapsedTime < fadeTime){
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }

    private IEnumerator FadeRoutine(Image image, float fadeTime, float startValue, float targetTransparency){
        float elapsedTime = 0;
        while(elapsedTime < fadeTime){
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            yield return null;
        }
    }
}
