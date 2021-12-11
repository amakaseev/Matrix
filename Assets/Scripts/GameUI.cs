using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI: MonoBehaviour {
  public GameObject[] hitpoints;

  void Start() {
    Actions.OnPlayerTakeDamage += OnPlayerTakeDamage;
  }

  void OnPlayerTakeDamage(int damage, int hitpoints) {
    SetHitpoints(hitpoints);
  }

  public void SetHitpoints(int hp) {
    for (int i = 0; i < hitpoints.Length; ++i) {
      if (i < hp) {
        hitpoints[i].SetActive(true);
      } else {
        hitpoints[i].SetActive(false);
      }
    }
  }

}
