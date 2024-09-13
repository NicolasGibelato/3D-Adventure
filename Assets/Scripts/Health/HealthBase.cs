using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cloth;


public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public float deathDuration = 0.2f;
    public bool destroyOnKill = false;
    [SerializeField] public float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIHealthUpdate> UIHealthUpdate;

    private float damageMultiply = 1;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        ResetLife();

    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
        Destroy(gameObject, deathDuration);

        OnKill?.Invoke(this);
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiply;

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(UIHealthUpdate != null)
        {
            UIHealthUpdate.ForEach(i => i.UpdadeValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damage, float duration)
    {

        StartCoroutine(ChangeDamageMultiplyCoroutine(damage, duration));
    }

    IEnumerator ChangeDamageMultiplyCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;
    }
}
