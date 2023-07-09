using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Worker.StateMachines.States
{
    public class PutsStone : IState
    {
        //[SerializeField] private Transform[] _targets;
        //[SerializeField] private Building _building;
        //[SerializeField] private Stone _heldStone;

        private Worker _worker;
        private Animator _animator;
        private bool _isStoneVisible; //виден ли камень
        
        private bool _isStoneTaken = false;

        public PutsStone(Worker worker, Animator animator)
        {
            _worker = worker;
            _animator = animator;
        }

        /*private void Update()
        {
            float distance = Vector3.Distance(transform.position, _targets[1].position);
            float distance2 = Vector3.Distance(transform.position, _targets[0].position);

            if (distance <= 0.2f && !_isStoneTaken)
            {
                _heldStone.gameObject.SetActive(false);
                _building.GetStone();
                _isStoneTaken = true;
                Debug.Log("сработало условие");
            }
            else if (distance2 < 0.2f)
            {
                _heldStone.gameObject.SetActive(true);
                _isStoneTaken = false;
            }
        }*/

        public void OnEnter()
        {
            _animator.SetBool(HashAnimator.IsStoneTaken, false);
            _isStoneVisible = false;
            _worker.PutStone(_isStoneVisible);
        }

        public void Tick()
        {
        }
        
        public void OnExit()
        {
            //_isStoneTaken = false;
            //_worker.PutStone(_isStoneVisible);        
        }
    }
}