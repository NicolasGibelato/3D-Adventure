using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aperture.StateMachine;
using DG.Tweening;


namespace Boss
{ 
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Animation")]
        public int attackAmount = 5;
        public float timeBetweenAttack = .5f;


        public float speed = 5f;
        public List<Transform> wayPoints;
        public GunBase gunBase;
        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            Init();
            OnValidate();
            if (healthBase != null)
            {
                healthBase.OnKill += OnBossKill;
            }
            
        }

       protected virtual void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        #region ATTACK
        public void StartAttack(Action endCallBack = null)
        {
            StartCoroutine(StartAttackCoroutine(endCallBack));
            gunBase.StartShoot();
        }

        IEnumerator StartAttackCoroutine(Action endCallBack)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                yield return new WaitForSeconds(timeBetweenAttack);
            }

            endCallBack?.Invoke();
        }
        #endregion


        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(wayPoints[UnityEngine.Random.Range(0, wayPoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion


        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion


        #region STATE MACHINE

        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }

        #endregion
    }
}