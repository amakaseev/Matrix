using System;

public static class Actions {

  public static Action<int> OnCardActive;
  public static Action<int, int> OnPlayerMoveFinish;
  public static Action<int, int> OnPlayerTakeDamage;
  public static Action OnPlayerDie;

}
