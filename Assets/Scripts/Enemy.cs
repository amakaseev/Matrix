using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {
  public float speed;

  void Start() {

  }

  void Update() {
    Vector3 position = transform.position;
    position.y -= speed * Time.deltaTime;

    if (position.y < -6) {
      Destroy(gameObject);
    }

    transform.position = position;
  }

}
