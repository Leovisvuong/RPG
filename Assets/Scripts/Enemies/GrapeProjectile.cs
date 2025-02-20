using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 3;
    [SerializeField] private GameObject grapeProjectileShadow;
    [SerializeField] private GameObject SplatterPrefab;

    private GameObject grapeShadow;

    private void Start(){
        grapeShadow = Instantiate(grapeProjectileShadow, transform.position + new Vector3(0,-0.3f, 0), Quaternion.identity);
        
        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(grapeShadow, grapeShadowStartPosition, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition){
        endPosition = new Vector3(endPosition.x,endPosition.y - 1,endPosition.z);
        
        float timePassed = 0;

        while(timePassed < duration){
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }
        GameObject instance = Instantiate(SplatterPrefab, transform.position, quaternion.identity);
        instance.transform.parent = this.transform.parent;
        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition){
        float timePassed = 0;

        while(timePassed < duration){
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            grapeShadow.transform.position = Vector2.Lerp(startPosition,endPosition, linearT);
            yield return null;
        }

        Destroy(grapeShadow);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerController>()){
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            playerHealth?.TakeDamage(GetComponentInParent<EnemyAI>().maxAttack,transform);
            Destroy(grapeShadow);
            Destroy(gameObject);
        }
        else if(other.gameObject.GetComponent<DamageSource>()){
            Destroy(grapeShadow);
            Destroy(gameObject);
        }
    }
}

