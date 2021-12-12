using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell: MonoBehaviour {

  public Vector2Int gridPos;
  public Vector2Int direction = new Vector2Int(1, 0);
  public int damage = 0;

  public void AddCard(int cardIndex) {
    // TODO: AddCard prefab to this cell
  }

}
