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
    Actions.OnPlayerWin += OnPlayerWin;
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
      GenerateLine(true);
    }
    for (int x = 0; x < 7; ++x) {
      GenerateLine(false);
    }
  }

  void GenerateLine(bool safe) {
    if (_lastLine >= 101) {
      return;
    }

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
      Destroy(cell.gameObject);
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

  void OnPlayerWin() {
    Time.timeScale = 0;
    gameObject.GetComponent<PlayerInput>().enabled = false;
  }
  
  void OnPlay() {
    Debug.Log("Play");

    GenerateLevel();
    player.Restart();
    cameraMover.Restart();
    Time.timeScale = 1;

    gameObject.GetComponent<PlayerInput>().enabled = false;
  }

  void OnPlayerMoveFinish() {
    var cell = GetGridCell(player.gridPosition);
    if (cell != null) {
      int damage = cell.damage;
      if (damage > 0) {
        player.TakeDamage(damage);
      }
    } else {
      player.TakeDamage(3);
    }

    // if (_cells.Count > gridHeight * 20) {
    //   RemoveLine();
    // }

    _timeToStep = 2;

    Actions.OnLineComplete(player.gridPosition.x);

    if (_cells.Count < gridHeight * 20) {
      GenerateLine(false);
    }
  }

  void Update() {
    _timeToStep -= Time.deltaTime;
    if (_timeToStep <= 0) {
      var prevPos = player.gridPosition;
      player.MoveTo(player.gridPosition + GetGridCell(player.gridPosition).direction);
      GetGridCell(prevPos).RemoveCard();
      _timeToStep = 2;
    }
  }

}
