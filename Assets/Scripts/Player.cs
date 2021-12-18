using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player: MonoBehaviour {

  public int hitpoints = 3;
  public float  moveSpeed = 2;
  public bool isMove = false;
  public bool isBoost = false;
  public bool isJump = false;
  public Vector2Int gridPosition;
  public Vector3 targetPosition;
  public Level level;
  public bool isDead = false;

  void Start () {
    Application.targetFrameRate = 60;
    Restart();
  }

  public void Restart() {
    hitpoints = 3;
    gridPosition.x = 0;
    gridPosition.y = 2;
    transform.position = new Vector3(gridPosition.x * 2, 0, gridPosition.y);
    Actions.OnPlayerUpdateHitpoints(hitpoints);
  }

  public void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("OnTriggerEnter2D: " + other.tag);
    if (other.tag == "Enemy") {
      Debug.Log(other.gameObject.GetComponent<Enemy>());
      // Actions.OnEnemyInteraction(other.gameObject.GetComponent<Enemy>());
    }
  }

  public void MoveTo(Vector2Int pos, bool boost) {
    isMove = true;
    isBoost = boost;
    gridPosition = pos;
    targetPosition = new Vector3(gridPosition.x * 2, 0, gridPosition.y);
  }

  public void JumpTo(Vector2Int pos) {
    isMove = true;
    isJump = true;
    gridPosition = pos;
    targetPosition = new Vector3(gridPosition.x * 2, 0, gridPosition.y);
  }

  public void TakeDamage(int damage) {
    hitpoints -= damage;
    Actions.OnPlayerTakeDamage(damage, hitpoints);
    Debug.Log("OnPlayerTakeDamage: " + damage + ", " + hitpoints);
    if (hitpoints <= 0) {
      isDead = true;
      Actions.OnPlayerDie();
    }
  }

  void Update() {
    if (isMove || isJump) {
      float step =  moveSpeed * Time.deltaTime * (isBoost? 2f : 1f);
      transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

      if (Vector3.Distance(transform.position, targetPosition) < 0.01f) {
        transform.position = targetPosition;
        isMove = false;
        isBoost = false;
        isJump = false;
        Actions.OnPlayerMoveFinish();
        if (gridPosition.x == 101) {
          isDead = true;
          Actions.OnPlayerWin();
        }
      }
    }
  }

}
