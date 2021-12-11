using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelCamera: MonoBehaviour {

  public float speed = 1;

  bool move;
  float deltaPosition;
  float prevPosition;

  public void OnMove(InputValue input) {
    move = true;
    prevPosition = transform.position.x;
    Debug.Log(input.Get<Vector2>());
  }

  void Update() {
    if (move) {
      deltaPosition += speed * Time.deltaTime;
      if (deltaPosition >= 1) {
        move = false;
        deltaPosition = 1;
      }
      Vector3 position = transform.position;
      position.x = prevPosition + deltaPosition;
      transform.position = position;
    }
  }

}
