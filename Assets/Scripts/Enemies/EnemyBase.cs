using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public ParticleSystem particleSystem;
        public Collider collider;
        public FlashColor flashColor;
        public float startLife = 10f;
        public bool lookAtPlayer = false;
       


        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;
        
        
        [Header("Start Animation")]
        public float startAnimationDuration = 1.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;
        public float deathDuration = 2f;

        [Header("Events")]
        public UnityEvent OnKillEvent;



        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();

            if(startWithBornAnimation)
            BornAnimation();
        }

        protected virtual void Kill()
        {

            OnKill();

        }

        protected virtual void OnKill() 
        {
            if (collider != null) collider.enabled = false;
            Destroy(gameObject, deathDuration);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        #region ANIMATIONS

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }
        #endregion


        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(10);

            transform.position -= transform.forward;

            _currentLife -= f;

            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}
