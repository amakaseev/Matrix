using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level: MonoBehaviour {

  public GridCell     emptyCell;
  public GridCell[]   gridCells;
  public GridCell[]   gridWals;
  public int          gridHeight;
  public Enemy[]      enemiesPrefabs;
  public Player       player;

  int             _lastLine;
  float           _timeToStep;
  List<GridCell>  _cells = new List<GridCell>();

  void Start() {
    GenerateLevel();

    _timeToStep = 2;

    Actions.OnPlayerMoveFinish += OnPlayerMoveFinish;
    Actions.OnPlayerDie += OnPlayerDie;
  }

  GridCell GetGridCell(Vector2Int pos) {
    for (int i = 0; i < _cells.Count; ++i) {
      if (_cells[i].gridPos == pos) {
        return _cells[i];
      }
    }
    return null;
  }

  void GenerateLevel() {
    for (int x = 0; x < 7; ++x) {
      GenerateLine(true);
    }
    for (int x = 0; x < 7; ++x) {
      GenerateLine(false);
    }
  }

  void GenerateLine(bool safe) {
    for (int y = 0; y < gridHeight; ++y) {
      GridCell cell;
      if (safe) {
        cell = Instantiate(gridCells[Random.Range(0, gridCells.Length)]);
      } else {
        float rnd = Random.Range(0, 100);
        if (rnd < 70) {
          cell = Instantiate(gridCells[Random.Range(0, gridCells.Length)]);
        } else if (rnd < 80) {
          cell = Instantiate(emptyCell);
        } else {
          cell = Instantiate(gridWals[Random.Range(0, gridWals.Length)]);
        }
      }
      cell.gridPos = new Vector2Int(_lastLine, y);
      cell.transform.parent = transform;
      cell.transform.position = new Vector3(_lastLine, 0, y * 2);

      _cells.Add(cell);
    }
    _lastLine++;
  }

  void RemoveLine() {
    for (int y = 0; y < gridHeight; ++y) {
      var cell = _cells[0];
      _cells.Remove(cell);
      Destroy(cell);
    }
  }

  void OnPlayerDie() {
    Time.timeScale = 0;
  }

  void OnPlayerMoveFinish(int x, int y) {
    int damage = GetGridCell(player.gridPosition).damage;
    if (damage > 0) {
      player.TakeDamage(damage);
    }

    RemoveLine();

    _timeToStep = 2;

    GenerateLine(false);
  }

  void Update() {
    _timeToStep -= Time.deltaTime;
    if (_timeToStep <= 0) {
      player.MoveTo(player.gridPosition + GetGridCell(player.gridPosition).direction);
      _timeToStep = 2;
    }
  }

}
