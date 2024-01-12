using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMobManager {
  public RunnerState MobState { get; }
  public void Enlist(IAgentController agent);
  public void Drop(IAgentController agent);
}

public enum RunnerState {
  Hold,
  Run,
  Win,
  Die
}
