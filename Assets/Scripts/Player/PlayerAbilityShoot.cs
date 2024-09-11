using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public GunBase gunBase;
    public GunBase secondGun;

    public Transform gunPosition_01;
    public Transform gunPosition_02;

    public FlashColor _flashColor;

    private GunBase _currentGun;

    private GunBase gunInstance_01;
    private GunBase gunInstance_02;

    protected override void Init()
    {
        base.Init();
        CreateGun();
        

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.ChangeGun.performed += ctx => ChangeGuns();
        inputs.Gameplay.ChangeGun2.performed += ctx => ChangeGuns2();
        
    }

    private void CreateGun()
    {
        gunInstance_01 = Instantiate(gunBase, gunPosition_01);
        gunInstance_01.transform.localPosition = gunInstance_01.transform.localEulerAngles = Vector3.zero;
        gunInstance_02 = Instantiate(secondGun, gunPosition_02);
        gunInstance_02.transform.localPosition = gunInstance_02.transform.localEulerAngles = Vector3.zero;
        SetActiveGun();
    }

    
    private void ChangeGuns()
    {
        if (_currentGun = secondGun) 
        {
            //_currentGun = Instantiate(secondGun, gunPosition_02);
            //_currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.z
            SetActiveGun();
            Debug.Log("GunChange");
        }
    }
    private void ChangeGuns2()
    {
        if (_currentGun = gunBase)
        {
           //_currentGun = Instantiate(gunBase, gunPosition_01);
           //_currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
            SetActiveGun2();

            Debug.Log("GunChange 2");
        }
    }


    private void StartShoot()
    {
        _currentGun.StartShoot();
        _flashColor?.Flash();
        Debug.Log("Start Shoot");
    } 
    
    private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot");
    }

    private void SetActiveGun()
    {
        gunPosition_01.gameObject.SetActive(false);
        gunPosition_02.gameObject.SetActive(true);
        _currentGun = gunInstance_02;
        Debug.Log("GunActive");

    }
    private void SetActiveGun2()
    {
        gunPosition_02.gameObject.SetActive(false);
        gunPosition_01.gameObject.SetActive(true);
        _currentGun = gunInstance_01;
        Debug.Log("GunActive2");

    }
}
