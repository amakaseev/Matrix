using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {

  public GameObject[] gridCells;
  public int          gridHeight;
  public Enemy[]      enemiesPrefabs;
  public float        spawnDelay;

  int   _lastLine;
  float _currentSpawnTime;

  void Start() {
    GenerateLevel();

    Actions.OnPlayerMoveFinish += OnPlayerMoveFinish;
  }

  void GenerateLevel() {
    int gridWidth = 10;
    for (int x = 0; x < gridWidth; ++x) {
      GenerateLine();
    }
  }

  void GenerateLine() {
    for (int y = 0; y < gridHeight; ++y) {
      var cell = Instantiate(gridCells[Random.Range(0, gridCells.Length)]);
      cell.transform.parent = transform;
      cell.transform.position = new Vector3(_lastLine, 0, y);
    }
    _lastLine++;
  }

  void OnPlayerMoveFinish(int x, int y) {
    GenerateLine();
  }

  void Update() {
    // _currentSpawnTime += Time.deltaTime;
    // while (_currentSpawnTime >= spawnDelay) {
    //   _currentSpawnTime -= spawnDelay;

    //   Enemy enemy = Instantiate(enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)]);
    //   enemy.transform.parent = transform;
    //   enemy.transform.position = new Vector3(Random.Range(-8, 8), 6, 0);
    // }
  }

}
