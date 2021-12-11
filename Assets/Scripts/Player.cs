using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player: MonoBehaviour {

  public float  moveSpeed = 2;
  public bool isMove = false;
  public Vector2Int gridPosition;
  public Vector3 targetPosition;

  void Start () {
    Application.targetFrameRate = 60;
    gridPosition.x = 0;
    gridPosition.y = 2;
    transform.position = new Vector3(gridPosition.x, 1, gridPosition.y);
  }

  public void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("OnTriggerEnter2D: " + other.tag);
    if (other.tag == "Enemy") {
      Debug.Log(other.gameObject.GetComponent<Enemy>());
      // Actions.OnEnemyInteraction(other.gameObject.GetComponent<Enemy>());
    }
  }

  public void OnMove(InputValue input) {
    if (!isMove) {
      var direction = input.Get<Vector2>();
      if (direction.x > 0) {
        MoveTo(gridPosition + new Vector2Int(1, 0));
      } else if (direction.x < 0) {
        MoveTo(gridPosition + new Vector2Int(-1, 0));
      } else if (direction.y > 0) {
        MoveTo(gridPosition + new Vector2Int(0, 1));
      } else if (direction.y < 0) {
        MoveTo(gridPosition + new Vector2Int(0, -1));
      }
    }
  }

  public void MoveTo(Vector2Int pos) {
    isMove = true;
    gridPosition = pos;
    targetPosition = new Vector3(gridPosition.x, 1, gridPosition.y);
  }

  void Update() {
    if (isMove) {
      float step =  moveSpeed * Time.deltaTime;
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

      if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
        transform.position = targetPosition;
        isMove = false;
        Actions.OnPlayerMoveFinish(gridPosition.x, gridPosition.y);
      }
    }
  }

}
