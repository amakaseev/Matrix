using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMover: MonoBehaviour {

  public float speed = 1;
  public Transform target;

  void Update() {
    Vector3 p1 = transform.position;
    Vector3 p2 = target.transform.position;
    if (Mathf.Abs(p1.x - p2.x) > 0.01f) {
      float step =  Mathf.Clamp(speed * Time.deltaTime, 0, 1);
      p1.x = Mathf.Lerp(p1.x, p2.x, step);
      transform.position = p1;
    }
  }

}
