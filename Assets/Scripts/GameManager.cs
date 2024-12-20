using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  beacon;

    [SerializeField] private TMP_Text timerText;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public const int GameDuration = 20 * 60; // 20 minutes
    public       int GameTime { get; private set; } = 0;

    public float CriticalChance     { get; private set; } = 0.1f;
    public float CriticalMultiplier { get; private set; } = 2.0f;

    public float FireRate         { get; private set; } = 0.1f;
    public float ReloadTime       { get; private set; } = 1.0f;
    
    public float Damage           { get; private set; } = 10.0f;
    public float DamageMultiplier { get; private set; } = 1.2f;

    public float DamageToEnemy
    {
        get
        {
            var damage = Damage;
            if (Random.value < CriticalChance)
            {
                damage *= CriticalMultiplier;
            }

            return damage;
        }
    }

    public float MovementSpeed { get; private set; } = 5.0f;
    public float BoostSpeed    { get; private set; } = 5.0f;

    public float BeaconOverload { get; private set; } = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(Timer());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    private IEnumerator SpawnEnemy()
    {
        var wait = new WaitForSeconds(5);
        while (true)
        {
            var x = Random.Range(-10, 10);
            var z = Random.Range(-10, 10);

            var instance = Instantiate(enemyPrefab, new Vector3(x, 1, z), Quaternion.identity);
            instance.GetComponent<EnemyController>().target = beacon;
            yield return wait;
        }
    }
    
    private IEnumerator Timer()
    {
        var wait = new WaitForSeconds(1);
        while (GameTime < GameDuration)
        {
            GameTime++;
            timerText.text = $"{GameTime / 60:00}:{GameTime % 60:00}";
            yield return wait;
        }
    }
}