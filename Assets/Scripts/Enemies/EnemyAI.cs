using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int maxAttack = 1;
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 5;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCoolDown = 2;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;
    private enum State{
        Roaming,
        Attacking
    }
    
    private Vector2 roamPosition;
    private float timeRoaming = 0;
    private State state;
    
    private EnemyPathfinding enemyPathfinding;
    
    private void Awake(){
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
        
    }
    
    private void Start(){
        roamPosition = GetRoamingPosition();
    }

    private void Update(){
        MovementStateControl();
    }

    private void MovementStateControl(){
        switch(state){
            default:
            case State.Roaming:
                Roaming();
            break;
            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming(){
        timeRoaming += Time.deltaTime;
        enemyPathfinding.MoveTo(roamPosition);

        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange){
            state = State.Attacking;
        }

        if(timeRoaming > roamChangeDirFloat){
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking(){
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange){
            state = State.Roaming;
        }

        if(attackRange != 0 && canAttack){
            canAttack = false;
            if(enemyType != null){
                (enemyType as IEnemy).Attack();
            }
            if(stopMovingWhileAttacking){
                enemyPathfinding.StopMoving();
            }
            else{
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCoolDownRoutine());
        }
    }

    private IEnumerator AttackCoolDownRoutine(){
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition(){
        timeRoaming = 0;
        return new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
    }
}
