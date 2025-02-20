using Unity.Mathematics;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;
    [SerializeField] private WeaponInfoManager weaponInfoManager;


    private Animator myAnimator;
    private AudioSource staffAttack;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake(){
        myAnimator = GetComponent<Animator>();
        staffAttack = GameObject.Find("Staff Attack").GetComponent<AudioSource>();
        GameObject weaponInfoContainer = GameObject.Find(weaponInfoManager.weaponInfo.weaponName + " Info Container");

        if(!weaponInfoContainer){
            weaponInfoContainer = Instantiate(weaponInfoManager.gameObject);
            weaponInfoContainer.name = weaponInfoManager.weaponInfo.weaponName + " Info Container";
            weaponInfoContainer.transform.parent = GameObject.Find("Managers").transform;
        }

        weaponInfoManager = weaponInfoContainer.GetComponent<WeaponInfoManager>();
    }

    private void Update(){
        if(FreezeManager.Instance.gamePause) return;
        MouseFollowWithOffset();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            staffAttack.Play();
            Stamina.Instance.UseStamina(weaponInfoManager.weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger(ATTACK_HASH);
        }
        else{
            Warning.Instance.DoWarn("Run Out Of Stamina!", Color.yellow);
        }
    }

    public void SpawnStaffProjectileAnimationEvent(){
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfoManager.weaponInfo.weaponRange);
    }

    public WeaponInfoManager GetWeaponInfoManager(){
        return weaponInfoManager;
    }

    private void MouseFollowWithOffset(){
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x){
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle);
        }
        else{
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}