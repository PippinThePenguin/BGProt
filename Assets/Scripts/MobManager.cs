using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

public class MobManager : MonoBehaviour, IMobManager {
  public RunnerState MobState { get; private set; }

  [SerializeField] private ObjectController _objController;
  [SerializeField] private GameObject _joinParticle;

  private List<IAgentController> _mobAgents;
  private Draw _board;
  private GameState _gameController;

  [Inject] 
  private void Construct(Draw drawBoard, GameState state) {
    _board = drawBoard;
    _gameController = state;
  }

  private void Start() {
    _board.OnFinish += UpdatePositions;
    _gameController.OnStart += StartRun;
    _gameController.OnWin += Win;
    _mobAgents = transform.GetComponentsInChildren<IAgentController>().ToList();
    foreach (var agent in _mobAgents) {
      agent.MyManager = this;
    }

    _objController.spawnCount = _mobAgents.Count;
  }

  

  public void Enlist(IAgentController agent) {
    if (agent == null || 
        _mobAgents.Contains(agent) ||
        agent.CurrState == RunnerState.Die) {
      return;
    } 
    _mobAgents.Add(agent);
    agent.SetState(MobState);
    agent.MyManager = this;
    agent.MyTransform.SetParent(transform);
    Instantiate(_joinParticle, agent.ParticlePoint);
  }

  public void Drop(IAgentController agent) {
    if (agent == null || !_mobAgents.Contains(agent)) {
      return;
    }

    _mobAgents.Remove(agent);
    if (_mobAgents.Count == 0) {
      _gameController.LoseGame();
    }
  }  

  public void UpdatePositions(Vector3[] dots) {
    _objController.spawnCount = _mobAgents.Count;
    
    for (int i = 0; i < _mobAgents.Count(); i++) {
      _mobAgents[i].GoTo(_objController.transform.GetChild(i), 0.5f);
    }
  }

  private void StartRun() {
    MobState = RunnerState.Run;
    foreach (var agent in _mobAgents) {
      agent.SetState(MobState);
    }
  }

  private void Win() {
    MobState = RunnerState.Win;
    foreach (var agent in _mobAgents) {
      agent.SetState(MobState);
    }
  }
}
