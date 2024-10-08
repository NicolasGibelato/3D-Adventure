using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aperture.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckpointBase> checkpoints;

    public bool HasCheckPoint()
    {
        LoadCheckPointFromSave();
        return lastCheckPointKey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if(i > lastCheckPointKey)
        {
            lastCheckPointKey = i;
        }
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
       var checkpoint =  checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }

    public void LoadCheckPointFromSave()
    {
        SaveCheckPoint((int)SaveManager.Instance.Setup.checkPointKey);
    }
}
