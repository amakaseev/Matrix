using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {

  public Enemy[] enemiesPrefabs;
  public float spawnDelay;

  float _currentSpawnTime;

  void Update() {
    _currentSpawnTime += Time.deltaTime;
    while (_currentSpawnTime >= spawnDelay) {
      _currentSpawnTime -= spawnDelay;

      Enemy enemy = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)]);
      enemy.transform.parent = transform;
      enemy.transform.position = new Vector3(Random.Range(-8, 8), 6, 0);

    }
  }

}
