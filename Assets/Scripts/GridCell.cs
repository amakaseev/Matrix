using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell: MonoBehaviour {

  public Vector2Int gridPos;
  public int damage = 0;
  Vector2Int  _direction = new Vector2Int(1, 0);
  LevelCard   _card;
 
  public Vector2Int direction {
    get {
      // TODO: return _card direction if present this
      return _direction;
    }
  }

  public void AddCard(LevelCard cardPrefab) {
    // TODO: AddCard prefab to this cell
    _card = Instantiate(cardPrefab);
    _card.transform.parent = transform;
    _card.transform.localPosition = new Vector3(0, 0.02f, 0);
  }

}
