using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class GuyController : MonoBehaviour, IAgentController {
  public event Action OnStateChange;

  public RunnerState CurrState { get; private set; }
  public IMobManager MyManager { get; set; }
  public Transform MyTransform { get { return transform; } }
  public Transform ParticlePoint { get { return _partTransform; } }

  [SerializeField] private Transform _partTransform;


  private void OnTriggerEnter(Collider other) {    
    var controller = other.GetComponent<IAgentController>();
    if (controller != null && MyManager != null) {
      MyManager.Enlist(controller);
    }
  }  

  public void SetState(RunnerState state) {
    CurrState = state;
    OnStateChange?.Invoke();
  }

  public void GoTo(Transform target, float time) {
    var tween = transform.DOMove(target.position, time);
    tween.OnUpdate(() => tween.ChangeEndValue(target.position, true));
  }

  async public void Die(float time) {
    SetState(RunnerState.Die);
    GetComponent<Collider>().enabled = false;
    DOTween.Kill(transform);
    MyManager.Drop(this);
    transform.parent = null;
    await Task.Delay((int)(time * 1000));
    Destroy(gameObject);
  }


}
