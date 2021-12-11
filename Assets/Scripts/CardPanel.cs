using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanel: MonoBehaviour {

  public void OnCardActive(int index) {
    Actions.OnCardActive(index);
  }

}
