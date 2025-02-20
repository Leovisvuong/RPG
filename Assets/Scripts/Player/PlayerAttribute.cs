public class PlayerAttribute : Singleton<PlayerAttribute>
{
    // [SerializeField] private AttributeUIManager attributeManager;
    public int health;
    public int stamina;
    public int attack;
    public int magic;
    public int pointRemain;

    protected override void Awake()
    {
        base.Awake();

        health = 20;
        stamina = 60;
        attack = 1;
        magic = 1;
        pointRemain = 3;
    }

    public void AddPoint(){
        pointRemain++;
        // attributeManager.UpdateValue();
    }
}
