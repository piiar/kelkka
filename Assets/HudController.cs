using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour {

    public RectTransform playerHealthBar;
    public RectTransform enemyHealthBar;
    public RectTransform progress;
    public GameObject progressObject;
    public Text money;

    void Awake() {
        Game.instance.SetHud(this);
    }

    // Use this for initialization
    void Start () {
    }

    public void AddProgress() {
        GameObject go = Instantiate(progressObject);
        go.transform.SetParent(progress.transform);
    }

    public void SetMoney(int amount) {
        money.text = "$ " + amount;
    }

    public void SetPlayerHealth(float healthPercent) {
        Vector3 scale = new Vector3(Mathf.Max(healthPercent, 0), 1f, 1f);
        playerHealthBar.transform.localScale = scale;
    }

    public void SetEnemyHealth(float healthPercent) {
        Vector3 scale = new Vector3(Mathf.Max(healthPercent, 0), 1f, 1f);
        playerHealthBar.transform.localScale = scale;
    }
}
