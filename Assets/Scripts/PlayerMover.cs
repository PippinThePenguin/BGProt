using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Dreamteck.Splines;
using Zenject;

public class PlayerMover : MonoBehaviour {
  [SerializeField] private float _moveSpeed;
  [SerializeField] private SplineFollower _follower;

  private Draw _input;
  private GameState _gameState;
  private bool _isRunning = false;

  [Inject] 
  private void Construct(Draw input, GameState state) {
    _input = input;
    _gameState = state;
  }
  
  private void Start() {
    _follower.followSpeed = _moveSpeed;
    _follower.follow = false;
    _follower.onEndReached += (double d) => _gameState.OnWin();
    _input.OnFinish += TryStart;
    _gameState.OnEnded += () => _isRunning = false;

  }

  private void Update() {
    _follower.follow = _isRunning;
  }

  private void TryStart(Vector3[] obj) {
    if (_isRunning) {
      return;
    } 
    _isRunning = true;
    _gameState.StartGame();
    _input.OnFinish -= TryStart;
  }

  
}
