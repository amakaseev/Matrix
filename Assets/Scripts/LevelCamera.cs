using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCamera: MonoBehaviour {

  public void OnMove(InputValue input) {
    _moveDirection = input.Get<Vector2>();
    Debug.Log(_moveDirection);
  }

}
