using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private WeaponInfoManager weaponInfoManager;

    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private Animator myAnimator;
    private AudioSource bowAttack;

    private void Awake(){
        myAnimator = GetComponent<Animator>();
        bowAttack = GameObject.Find("Bow Attack").GetComponent<AudioSource>();
        GameObject weaponInfoContainer = GameObject.Find(weaponInfoManager.weaponInfo.weaponName + " Info Container");
        
        if(!weaponInfoContainer){
            weaponInfoContainer = Instantiate(weaponInfoManager.gameObject);
            weaponInfoContainer.name = weaponInfoManager.weaponInfo.weaponName + " Info Container";
            weaponInfoContainer.transform.parent = GameObject.Find("Managers").transform;
        }

        weaponInfoManager = weaponInfoContainer.GetComponent<WeaponInfoManager>();
    }

    public void Attack(){
        if(Stamina.Instance.currentStamina > 0){
            bowAttack.Play();
            Stamina.Instance.UseStamina(weaponInfoManager.weaponInfo.weaponStaminaCost);
            myAnimator.SetTrigger(FIRE_HASH); 
            GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
            newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfoManager.weaponInfo.weaponRange);
        }
        else{
            Warning.Instance.DoWarn("Run Out Of Stamina!", Color.yellow);
        }
    }
    public WeaponInfoManager GetWeaponInfoManager()
    {
        return weaponInfoManager;
    }
}
