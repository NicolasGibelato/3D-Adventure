using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Typper typper;
    public int key = 01;



    private bool checkPointActived = false;
    private bool checkPointActivedUI = true;
    private string checkPointKey = "CheckpointKey";

    private void OnTriggerEnter(Collider other)
    {
        if (!checkPointActived && other.transform.tag == "Player")
        {
            
            CheckCheckPoint();
            typper.StartType();

        }
    }

    private void CheckCheckPoint()
    {
        TurnItOn();
        SaveCheckPoint();
    }

    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);

    }

    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.gray);
    }

    private void SaveCheckPoint()
    {
        /*if (PlayerPrefs.GetInt(checkPointKey, 0) > key)
            PlayerPrefs.SetInt(checkPointKey, key);*/

        CheckPointManager.Instance.SaveCheckPoint(key);

        checkPointActived = true;
    }

}
