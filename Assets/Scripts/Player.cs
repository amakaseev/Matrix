using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player: MonoBehaviour {

  public Vector2 speed;

  Vector2 _moveDirection = Vector3.zero;

  void Start () {
    Application.targetFrameRate = 60;
  }

  public void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("OnTriggerEnter2D: " + other.tag);
    if (other.tag == "Enemy") {
      Debug.Log(other.gameObject.GetComponent<Enemy>());
      // Actions.OnEnemyInteraction(other.gameObject.GetComponent<Enemy>());
    }
  }

  public void OnMove(InputValue input) {
    _moveDirection = input.Get<Vector2>();
    Debug.Log(_moveDirection);
  }

  void Update() {
    if ((_moveDirection.x != 0) || (_moveDirection.y != 0)) {
      Vector3 position = transform.position;
      position.x += speed.x * _moveDirection.x * Time.deltaTime;
      position.y += speed.y * _moveDirection.y * Time.deltaTime;
      transform.position = position;
    }
  }

}
