using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public SFXType sfxType;
    public List<GameObject> endGameObjects;

    public int currentLevel;
    public int loadLevel = 0;


    private bool _endGame = false;

    [Header("sound")]
    public AudioSource audioSource;
    private string level;

    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));

    }
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (!_endGame && p != null)
        {
            ShowEndGame();
            PlaySFX();
            Invoke(nameof(LoadLevel), 1.5f);
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;
        endGameObjects.ForEach(i => i.SetActive(false));

        foreach(var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, 2f).SetEase(Ease.OutBack).From();
            SaveManager.Instance.SaveLastLevel(currentLevel);
        }
    }

    private void PlaySFX()
    {
        SFXPool.Instance.Play(sfxType);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(loadLevel);
    }
}