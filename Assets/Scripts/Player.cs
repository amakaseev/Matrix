using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

  Vector2 _moveDirection = Vector3.zero;

  void Start () {
    Application.targetFrameRate = 60;
  }

  public void OnMove(InputValue input) {
    _moveDirection = input.Get<Vector2>();
    Debug.Log(_moveDirection);
  }

}
