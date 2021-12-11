using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour {
  public float speed;

  void OnEnable() {
    //Actions.OnCardActive += OnCardActive;
  }

  void OnDisable() {
    //Actions.OnCardActive -= OnCardActive;
  }

  virtual public void OnCardActive(int index) {
    Debug.Log("OnCardActive: " + index);
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
