using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
    
    public bool FacingLeft {get { return facingLeft; } }
    public bool playerDied;
    
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private Knockback knockback;
    private AudioSource walk;
    private AudioSource dash;
    private float startingMoveSpeed;
    private bool facingLeft = false;
    private bool isDashing = false;
    protected override void Awake(){
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockback = GetComponent<Knockback>();
        walk = GameObject.Find("Walk").GetComponent<AudioSource>();
        dash = GameObject.Find("Dash").GetComponent<AudioSource>();
    }

    private void Start(){
        playerControls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;

        ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable(){
        playerControls.Disable();
    }

    private void Update() {
        if(playerDied || FreezeManager.Instance.gamePause) return;
        PlayerInput();
    }

    private void FixedUpdate() {
        if(playerDied || FreezeManager.Instance.gamePause) return;
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider(){
        return weaponCollider;
    }

    private void PlayerInput() {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("moveX",movement.x);
        myAnimator.SetFloat("moveY",movement.y);
    }

    private void Move() {
        if(knockback.GettingKnockedBack){
            return;
        }
        
        string currentAnimator = myAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if(currentAnimator == "Running" && !walk.isPlaying){
            walk.Play();
        }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
    private void AdjustPlayerFacingDirection(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePos.x < playerScreenPoint.x){
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else{
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash(){
        if(!isDashing && !FreezeManager.Instance.gamePause && !playerDied){
            if(Stamina.Instance.currentStamina <= 0){
                Warning.Instance.DoWarn("Run Out Of Stamina!",Color.yellow);
                return;
            }

            dash.Play();
            Stamina.Instance.UseStamina(1);
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine(){
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
