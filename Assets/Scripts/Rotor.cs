using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotor : MonoBehaviour {
  [SerializeField] private float _speed;

  
  void Update() {
    transform.Rotate(0f, 0f, _speed * Time.deltaTime); 
  }
}
