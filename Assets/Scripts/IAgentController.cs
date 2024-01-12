using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentController {
  public event Action OnStateChange;
  public RunnerState CurrState { get; }
  public IMobManager MyManager { get; set; }
  public Transform MyTransform { get; }
  public Transform ParticlePoint { get; }

  public void SetState(RunnerState state);
  public void GoTo(Transform target, float time);
  public void Die(float time);
}
