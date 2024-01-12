using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState {
  public Action OnStart;
  public Action OnLose;
  public Action OnWin;
  public Action OnEnded;

  private bool isStarted = false;
  private bool isEnded = false;

  public void StartGame() {
    if (isStarted || isEnded) {
      return;
    }
    isStarted = true;
    OnStart?.Invoke();
  }

  public void LoseGame() {
    if (!isStarted || isEnded) {
      return;
    }
    isEnded = true;
    OnLose?.Invoke();
    OnEnded?.Invoke();
  }

  public void WinGame() {
    if (!isStarted || isEnded) {
      return;
    }
    isEnded = true;
    OnWin?.Invoke();
    OnEnded?.Invoke();
  }
}
