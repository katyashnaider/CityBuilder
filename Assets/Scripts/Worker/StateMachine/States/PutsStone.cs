using System;
using UnityEngine;

namespace Worker.StateMachines.States
{
    public class PutsStone : MonoBehaviour
    {
        [SerializeField] private Building _building;
        [SerializeField] private Stone _stoneInHands;
        
        private void Start()
        {
            _stoneInHands.gameObject.SetActive(true);
            _building.GetStone();
        }
    }
}