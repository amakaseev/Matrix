using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI: MonoBehaviour {
  public GameObject[] hitpoints;
  public Text         linesCount;
  public GameObject   gameOverPanel;

  void Start() {
    gameOverPanel.SetActive(false);
    linesCount.text = "0";

    Actions.OnLineComplete += OnLineComplete;
    Actions.OnPlayerUpdateHitpoints += OnPlayerUpdateHitpoints;
    Actions.OnPlayerTakeDamage += OnPlayerTakeDamage;
    Actions.OnPlayerDie += OnPlayerDie;
  }

  void OnLineComplete(int line) {
    linesCount.text = line.ToString();
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
    linesCount.text = "0";

    Actions.OnPlay();
  }

}
