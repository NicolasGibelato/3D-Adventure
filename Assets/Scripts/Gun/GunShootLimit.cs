using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdate> uIGunUpdaters;


    public float maxShoot = 5f;
    public float timeToReload = 1.5f;

    private float _currentShoots;
    private bool _reload = false;

    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if (_reload) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {

                Shoot();
                _currentShoots++;
                CheckReload();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckReload()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartReload();
        }
    }

    private void StartReload()
    {
        _reload = true;
        StartCoroutine(ReloadCoroutine());
    }

    IEnumerator ReloadCoroutine()
    {
        float time = 0;
        while (time < timeToReload)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdadeValue(time/timeToReload));
            yield return new WaitForEndOfFrame();
        }
        _currentShoots = 0;
        _reload = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        uIGunUpdaters = GameObject.FindObjectsOfType<UIFillUpdate>().ToList();
    }
}
