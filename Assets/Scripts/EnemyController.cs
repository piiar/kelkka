using UnityEngine;
using LitJson;

public class EnemyController : MonoBehaviour {

    public string userId { get; private set; }
    private string userName = null;
    private string color = "#ffffff";
    private Vector3 movement = Vector3.zero;
    private CharacterController controller;
    private int health = 30;

    public int speedClass; //Bottom -- 20*modifier 
    public Weapon weapon_left;
    public Weapon weapon_right;
    private AiMode aiMode;

    public ParticleSystem sparkEmitter;

    public GameObject rubberWheels;
    public GameObject trackWheels;
    public GameObject metallicWheels;

    public GameObject shieldLeft;
    public GameObject flamerLeft;
    public GameObject zapperLeft;

    public GameObject shieldRight;
    public GameObject flamerRight;
    public GameObject zapperRight;

    public SpawnPointGenerator spawnPointGenerator;
    // Use this for initialization
    void Start() {   
        controller = GetComponent<CharacterController>();

        transform.position = spawnPointGenerator.RandomSpawnPoint();

        /*
        //obj_Robo01_
        foreach (Transform child in transform)
        {
            Debug.Log("Iterating on child: " + child.name);
            if (child.name == "obj_Robo01_")
            {
                Debug.Log("Changing color to robot: " + color);
                child.GetComponent<Material>().SetColor("_MaskColor", HexToColor(color));
            }

        }
        */
    }

    void OnDestroy() {
        if (userId != null) {
            EventManager.StopListening(userId, OnMessage);
        }
    }

    public void AddDamage(int damage, Vector3 direction) {
        health -= damage;

        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        //sparkEmitter.transform.rotation = targetRotation;
        //sparkEmitter.Emit(10);

        if(health <= 0) {
            // TODO
            print("died");
        }
    }

    public void SetUserId(string id) {
        userId = id;
        EventManager.StartListening(userId, OnMessage);
    }

    public void SetName(string name) {
        userName = name;
    }

    public void SetColor(string color)
    {
        this.color = color;
    }

    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }

    void OnMessage(NetworkAction message) {
        Debug.Log("player " + userId + " got message " + message);
    }

    public void InitEquipment(int top, int bottom, int left, int right)
    {
        weapon_left = weaponLeft(left);
        weapon_right = weaponRight(right);
        aiMode = aiFor(top);

        /*if (aiMode == AiMode.Aggressive || aiMode == AiMode.Flanking)
        {
            GetComponent<WatcherRobotMovement>().SetMovementTarget(GameObject.FindGameObjectWithTag("Player").transform);
        }
        */
        //movement
        setupWheels(bottom);
    }

    private Weapon weaponLeft(int i)
    {
        switch (i)
        {
            case 0:
                shieldLeft.SetActive(true);
                return new Weapon("Shield", 0);
            case 1:
                flamerLeft.SetActive(true);
                return new Weapon("FlameThrower", 10);
            default:
                zapperLeft.SetActive(true);
                return new Weapon("Zap", 20);
        }
    }

    private Weapon weaponRight(int i) {
        switch(i) {
            case 0:
                shieldRight.SetActive(true);
                return new Weapon("Shield", 0);
            case 1:
                flamerRight.SetActive(true);
                return new Weapon("FlameThrower", 10);
            default:
                zapperRight.SetActive(true);
                return new Weapon("Zap", 20);
        }
    }

    private void setupWheels(int i) {
        speedClass = i;
        switch(i) {
            case 0:
                metallicWheels.SetActive(true);
                break;
            case 1:
                trackWheels.SetActive(true);
                break;
            default:
                rubberWheels.SetActive(true);
                break;
        }
    }

    private AiMode aiFor(int i)
    {
        switch (i)
        {
            case 0:
                return AiMode.Aggressive; 
            case 1:
                return AiMode.Flanking;
            default:
                return AiMode.Objective;
        }
    }

    public AiMode GetAIMode()
    {
        return aiMode;
    }

    public float MoveSpeed()
    {
        switch (speedClass)
        {
            case 0:
                return 1.0f;
            case 1:
                return 1.3f;
            default:
                return 1.5f;
        }
    }

    public void ChangeRobot(NetworkAction action) {
        JsonData data = JsonMapper.ToObject(action.data);
        JsonData robotStructure = data["robot"];
        Debug.Log("RoboStruct : " + robotStructure.ToString());
        // TODO
    }
}
