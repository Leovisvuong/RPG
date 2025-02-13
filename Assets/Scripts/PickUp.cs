using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.ProjectWindowCallback;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private enum PickUpType{
        GoldCoin,
        StamineGlobe,
        HealthGlobe
    }

    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5;
    [SerializeField] private float accelartionRate = 0.2f;
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heighty = 1.5f;
    [SerializeField] private float popDuration = 1f;

    private Vector3 moveDir;
    private Rigidbody2D rb;
    private GameObject player;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
        player = PlayerController.Instance.gameObject;
    }

    private void Start(){
        StartCoroutine(AnimationCurveSpawnPoint());
    }

    private void Update(){
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if(Vector3.Distance(transform.position,playerPos) < pickUpDistance){
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        }

        else{
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate(){
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.GetComponent<PlayerController>()){
            DetectPickUpType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimationCurveSpawnPoint(){
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f,2f);
        float randomY = transform.position.y + Random.Range(-1f,1f);

        Vector2 endPoint = new Vector2(randomX,randomY);

        float timePassed = 0f;

        while(timePassed < popDuration){
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heighty, heightT);

            transform.position = Vector2.Lerp(startPoint,endPoint,linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void DetectPickUpType(){
        switch(pickUpType){
            case PickUpType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold(1);
                break;
            case PickUpType.HealthGlobe:
                player.GetComponent<PlayerHealth>().HealPlayer(1);
                break;
            case PickUpType.StamineGlobe:
                Stamina.Instance.RefreshStamina(5);
                break;
            
        }
    }
}
