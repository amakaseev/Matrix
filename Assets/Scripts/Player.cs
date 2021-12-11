using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

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

}
