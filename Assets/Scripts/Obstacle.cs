using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
  [SerializeField] private float _delay;
  [SerializeField] private GameObject _deathEffect;
  private void OnTriggerEnter(Collider other) {
    var agent = other.GetComponent<IAgentController>();
    if (agent != null) {
      agent.Die(_delay);
      Instantiate(_deathEffect, agent.ParticlePoint);
    }
  }
}
