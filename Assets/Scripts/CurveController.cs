using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

public class CurveController : MonoBehaviour {
  [SerializeField] private SplineComputer _splineComputer;
  [SerializeField] private Vector2 _formationSize;

  private Draw _drawBoard;

  private Vector2 _converionScale;

  [Inject]
  private void Construct(Draw source) {
    _drawBoard = source;
  }

  private void Start() {
    _drawBoard.OnFinish += Redraw;
    CalculateScale();

  }

  private void CalculateScale() {
    _converionScale = _formationSize / _drawBoard.GetSize();
  }

  private void Redraw(Vector3[] points) {
    SplinePoint[] splinepoints = new SplinePoint[points.Length];
    for (int i = 0; i < points.Length; i++) {
      splinepoints[i].position = points[i] * _converionScale;
    }
    
    _splineComputer.SetPoints(splinepoints, setSpace: SplineComputer.Space.Local);
  }
}
