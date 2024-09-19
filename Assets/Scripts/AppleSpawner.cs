using System.Collections;
using UnityEngine;

public class AppleSpawner : MonoBehaviour {

    [SerializeField] GameObject applePrefab;
    [SerializeField] float spawnDistance = 12f;
    [SerializeField] float delayForNextApples = 1f;

    void Start() {
        StartCoroutine(SpawnApples());
    }

    IEnumerator SpawnApples() {
        while (true) {
            yield return new WaitForSeconds(delayForNextApples);

            int randomNumber = Random.Range(1, 10);

            for (int i = 0; i < randomNumber; i++) {
                Vector2 spawnPosition = Random.insideUnitCircle * spawnDistance;
                Instantiate(applePrefab, spawnPosition, Quaternion.identity, transform);
            }

            yield return new WaitUntil(() => transform.childCount == 0);
        }
    }
}
