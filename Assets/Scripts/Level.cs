using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Level: MonoBehaviour {

  public GridCell     emptyCell;
  public GridCell[]   gridCells;
  public GridCell[]   gridWals;
  public int          gridHeight;
  public Enemy[]      enemiesPrefabs;
  public Player       player;
  public CameraMover  cameraMover;
  public LevelCard[]  cardsPrefabs;

  int             _lastLine;
  float           _timeToStep;
  List<GridCell>  _cells = new List<GridCell>();
  Vector2         _selectPosition;
  int             _currentCard = -1;

  void Start() {
    GenerateLevel();

    Actions.OnPlay += OnPlay;
    Actions.OnPlayerMoveFinish += OnPlayerMoveFinish;
    Actions.OnPlayerDie += OnPlayerDie;
    Actions.OnCardActive += OnCardActive;
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
    while (_cells.Count > 0) {
      RemoveLine();
    }

    _lastLine = 0;
    _timeToStep = 2;

    for (int x = 0; x < 7; ++x) {
      GenerateLine(false);
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
      cell.transform.position = new Vector3(_lastLine * 2, 0, y);

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

  void OnMove(InputValue input) {
    _selectPosition = input.Get<Vector2>();
  }

  void OnSelect(InputValue input) {
    if (_currentCard != -1 && !EventSystem.current.IsPointerOverGameObject()) {
      RaycastHit hitInfo = new RaycastHit();
      bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(_selectPosition), out hitInfo);
      if (hit && hitInfo.transform.tag == "Cell") {
        Debug.Log("OnSelect " + hitInfo.transform.gameObject);
        // TODO: set card to this cell
        hitInfo.transform.gameObject.GetComponent<GridCell>().AddCard(cardsPrefabs[_currentCard]);
      }
    }
  }

  void OnCardActive(int index) {
    _currentCard = index;
  }

  void OnPlayerDie() {
    Time.timeScale = 0;
    gameObject.GetComponent<PlayerInput>().enabled = false;
  }

  void OnPlay() {
    Debug.Log("Play");

    GenerateLevel();
    player.Restart();
    cameraMover.Restart();
    Time.timeScale = 1;
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
      _timeToStep = 1;
    }
  }

}
