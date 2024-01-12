using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IAgentController))]
public class GuyAnimationController : MonoBehaviour {
  [SerializeField] private Animator _animator;

  private IAgentController _guy;
  private void Awake() {
    _guy = GetComponent<IAgentController>();
    _guy.OnStateChange += ChangeAnim;
  }

  private void ChangeAnim() {
    RunnerState state = _guy.CurrState;
    string trigger = "";
    switch (state) {
      case RunnerState.Run:
        trigger = "ToRun";
        break;
      case RunnerState.Die:
        trigger = "ToDie";
        break;
      case RunnerState.Win:
        trigger = "ToWin";
        break;
      default:
        trigger = "ToIdle";
        break;
    }
    _animator.SetTrigger(trigger);
  }
}
