using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform  beacon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(SpawnEnemy());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible   = false;
    }

    private System.Collections.IEnumerator SpawnEnemy()
    {
        var wait = new WaitForSeconds(2);
        while (true)
        {
            var x = Random.Range(-10, 10);
            var z = Random.Range(-10, 10);

            var instance = Instantiate(enemyPrefab, new Vector3(x, 1, z), Quaternion.identity);
            instance.GetComponent<EnemyController>().target = beacon;
            yield return wait;
        }
    }
}