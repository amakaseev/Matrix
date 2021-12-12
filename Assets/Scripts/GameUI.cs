using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI: MonoBehaviour {
  public GameObject[] hitpoints;
  public GameObject   gameOverPanel;

  void Start() {
    gameOverPanel.SetActive(false);

    Actions.OnPlayerUpdateHitpoints += OnPlayerUpdateHitpoints;
    Actions.OnPlayerTakeDamage += OnPlayerTakeDamage;
    Actions.OnPlayerDie += OnPlayerDie;
  }

  void OnPlayerUpdateHitpoints(int hitpoints) {
    SetHitpoints(hitpoints);
  }

  void OnPlayerTakeDamage(int damage, int hitpoints) {
    SetHitpoints(hitpoints);
  }

  void OnPlayerDie() {
    gameOverPanel.SetActive(true);
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

  public void Play() {
    gameOverPanel.SetActive(false);

    Actions.OnPlay();
  }

}
