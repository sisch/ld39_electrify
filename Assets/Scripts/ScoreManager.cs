using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public float maxPower;
    public float power;
    public float score;
    public float pointsPerSecond = 200.0f;
    public Text scoreText;
    float drainSpeed;
    int level;
    public bool isRunning;

    [SerializeField] float startPower;

    // Use this for initialization
    void Awake()
    {
        isRunning = true;
        power = startPower > 0 ? startPower : maxPower;
        if (Instance)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        power -= Time.deltaTime;
        if (power <= 0)
        {
            isRunning = false;
            GameManager.Instance.EndGame();
        }
        if (isRunning)
        {
            scoreText.text = string.Format("Score: {0:000}", score);
        }
    }

    public void Score()
    {
        if (!isRunning)
        {
            return;
        }
        score += pointsPerSecond * Time.deltaTime;
    }

    public void DrainPower()
    {
        power -= 1f;
    }
}