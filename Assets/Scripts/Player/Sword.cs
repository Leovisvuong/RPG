using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject SlashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;

    [SerializeField] private WeaponInfoManager weaponInfoManager;
    private Transform weaponCollider;
    private Animator myAnimator;
    private GameObject slashAnimation;
    private AudioSource swordAttack;
    private bool findDone = false;
    private void Awake(){
        myAnimator = GetComponent<Animator>();
        swordAttack = GameObject.Find("Sword Attack").GetComponent<AudioSource>();
        GameObject weaponInfoContainer = GameObject.Find(weaponInfoManager.weaponInfo.weaponName + " Info Container");

        if(!weaponInfoContainer){
            weaponInfoContainer = Instantiate(weaponInfoManager.gameObject);
            weaponInfoContainer.name = weaponInfoManager.weaponInfo.weaponName + " Info Container";
            weaponInfoContainer.transform.parent = GameObject.Find("Managers").transform;
        }

        weaponInfoManager = weaponInfoContainer.GetComponent<WeaponInfoManager>();
    }

    private void Start(){
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimationSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }
    
    private void Update(){
        if(FreezeManager.Instance.gamePause) return;
        MouseFollowWithOffset();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            swordAttack.Play();
            Stamina.Instance.UseStamina(weaponInfoManager.weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnimation = Instantiate(SlashAnimationPrefab,slashAnimationSpawnPoint.position,Quaternion.identity);
            slashAnimation.transform.parent = this.transform.parent;
        }
        else{
            Warning.Instance.DoWarn("Run Out Of Stamina!", Color.yellow);
        }
    }

    public void DoneAttackAnimationEvent(){
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent(){
        slashAnimation.transform.rotation = Quaternion.Euler(-180,0,0);

        if(PlayerController.Instance.FacingLeft){
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimationEvent(){
        slashAnimation.transform.rotation = Quaternion.Euler(0,0,0);

        if(PlayerController.Instance.FacingLeft){
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = 30;

        if(mousePos.x < playerScreenPoint.x){
            ActiveWeapon.Instance.gameObject.transform.rotation = Quaternion.Euler(0,-180,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,-180,0);
        }
        else{
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    public WeaponInfoManager GetWeaponInfoManager()
    {
        return weaponInfoManager;
    }
}
