using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Draw : MonoBehaviour {
  public event Action<Vector3[]> OnFinish;

  [SerializeField] private GameObject _brush;  
  [SerializeField] private RectTransform _field;

  private LineRenderer _lineRenderer;
  private Vector2 _lastPos;
  private bool _newTouch = true;



  private void Awake() {
    _lineRenderer = _brush.GetComponent<LineRenderer>();    
  }

  private void Update() {
    Drawing();
  }

  public Vector2 GetSize() {
    return new Vector2(_field.rect.width, _field.rect.height);
  }

  private void Drawing() {
    if (Input.touchCount > 0 && _newTouch) {
      _newTouch = false;
      StartDraw();
    } else if (Input.touchCount > 0) {
      PointToTouchPos();
    } else {
      _newTouch = true;
      FinishDraw();
    }
  }

  private void FinishDraw() {
    if (_lineRenderer.positionCount == 0) {
      return;
    }
    Vector3[] pos = new Vector3[_lineRenderer.positionCount];
    _lineRenderer.GetPositions(pos);
    OnFinish?.Invoke(pos);
    _lineRenderer.positionCount = 0;
  }

  private void StartDraw() {
    Vector2 touchPos = Input.touches[0].position;
    if (!InBounds(touchPos)) {
      return;
    }
    _lineRenderer.positionCount = 2;

    

    _lineRenderer.SetPosition(0, touchPos);
    _lineRenderer.SetPosition(1, touchPos);

  }

  private void AddAPoint(Vector2 pointPos) {
    _lineRenderer.positionCount++;
    int positionIndex = _lineRenderer.positionCount - 1;
    _lineRenderer.SetPosition(positionIndex, pointPos);
  }

  private void PointToTouchPos() {
    Vector2 touchPos = Input.touches[0].position;
    if (InBounds(touchPos) && _lastPos != touchPos) {
      AddAPoint(touchPos);
      _lastPos = touchPos;
    }
  }


  private bool InBounds(Vector2 pos) {    
    Vector3[] corn = new Vector3[4];
    _field.GetLocalCorners(corn);

    if (pos.x < (corn[2].x)
        && pos.x > (corn[0].x)
        && pos.y < (corn[2].y)
        && pos.y > (corn[0].y)) {
      return true;
    }
      
    return false;

  }
}
