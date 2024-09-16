using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Aperture.Core.Singleton;
using Cloth;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";

    public int lastLevel;
  

    public int lastCheckPointKey = 01;
    public Action<SaveSetup> FileLoaded;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Nicolas";
        Save();
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    #region SAVE
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
    }

    public void SaveItems()
    {
        _saveSetup.coins = Items.ItemManager.Instance.GetItemByType(Items.ItemType.COIN).soInt.value;
        _saveSetup.health = Items.ItemManager.Instance.GetItemByType(Items.ItemType.LIFE_PACK).soInt.value;
        Save();
    }

    public void SaveCheckPoints()
    {
        _saveSetup.checkPoints = CheckPointManager.Instance.HasCheckPoint();
        _saveSetup.checkPointPosition = CheckPointManager.Instance.GetPositionFromLastCheckPoint();
        _saveSetup.checkPointKey = CheckPointManager.Instance.lastCheckPointKey;
        Save();
    }

    public void SaveCloth()
    {
        _saveSetup.texture = Cloth.ClothManager.Instance.GetSetupByType(Cloth.ClothType.STRONG).texture;
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.playerName = text;
        Save();
    }


    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveCloth();
        SaveCheckPoints();
        SaveItems();
        Save();
    }
    #endregion
    private void SaveFile(string json)
    {
        File.WriteAllText(_path, json);
    }

    public void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);

            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }


        FileLoaded.Invoke(_saveSetup);
    }

    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

}


[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public float coins;
    public float health;
    public bool checkPoints;
    public Vector3 checkPointPosition;
    public float checkPointKey;
    public Texture2D texture;
    
    
    



    public string playerName;
}