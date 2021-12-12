using System;

public static class Actions {

  public static Action OnPlay;
  public static Action<int> OnLineComplete;
  public static Action<int> OnCardActive;
  public static Action<int, int> OnPlayerMoveFinish;
  public static Action<int, int> OnPlayerTakeDamage;
  public static Action<int> OnPlayerUpdateHitpoints;
  public static Action OnPlayerDie;

}
