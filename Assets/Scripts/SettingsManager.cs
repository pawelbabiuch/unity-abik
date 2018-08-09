using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingsManager : MonoBehaviour
{

    public float test = 0;

    private const int MAX_QUALITY_LVL = 5;
    private float soundValue = 0;

    [Header("Audio")]
    public AudioMixer aduioMixer;

    [Header("UI")]
    public Toggle soundToggle;
    public Slider volumeSlidger, levelLoadSlidger;

    public void ChangeQuality(int qualityId)
    {
        try
        {
            if(qualityId < 0 || qualityId > MAX_QUALITY_LVL)
            {
                throw new Exception("Błędne ID jakości grafiki.");
            }

            QualitySettings.SetQualityLevel(qualityId);

        }catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void TurnOnOffSound()
    {
        if(soundToggle.isOn)
        {
            volumeSlidger.interactable = true;
            volumeSlidger.value = soundValue;
        }
        else
        {
            soundValue = volumeSlidger.value;
            volumeSlidger.value = 0;
            volumeSlidger.interactable = false;
        }

        ChangeEffectsVolume();
    }

    public void ChangeEffectsVolume()
    {
        aduioMixer.SetFloat("masterVolume", volumeSlidger.value * 100 - 80);
    }

    public void LoadLevel(int sceneID)
    {
        try
        {
            levelLoadSlidger.gameObject.SetActive(true);
            //SceneManager.LoadScene(sceneID);
            StartCoroutine(LoadAsyncLevel(sceneID));
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private IEnumerator LoadAsyncLevel(int sceneID)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneID);

        do
        {
            levelLoadSlidger.value = async.progress;
            yield return null;
        } while (!async.isDone);

    }

    public void ExitGame()
    {
        Debug.Log("Gra została wyłączona");
        Application.Quit();
    }
}
