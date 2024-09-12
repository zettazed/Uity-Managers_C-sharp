using System.Collections;
using UnityEngine;
using YG;
using UnityEngine.UI;
using I2.Loc;

public class AdTimerManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject _timerPanel;
    [SerializeField] private Text _timerText;

    [Header("Localization")]
    [SerializeField] private string _timerTextEn = "Advertising via";
    [SerializeField] private string _timerTextRu = "Реклама через";
    [SerializeField] private string _timerTextTr = "Şununla reklam ver";

    [Header("Options")]
    [SerializeField] private bool nowAdsShow = false;
    [SerializeField] private float fullscreenAdInterval;

    private void Awake()
    {
        fullscreenAdInterval = YandexGame.Instance.infoYG.fullscreenAdInterval;
    }

    public void Update()
    {
        if (!YandexGame.nowAdsShow && YandexGame.timerShowAd >= fullscreenAdInterval && !nowAdsShow)
        {
            StartCoroutine(Timer());
        }
    }

    public IEnumerator Timer()
    {
        nowAdsShow = true;
        _timerPanel.SetActive(true);
        TimerTextChange(3);
        yield return new WaitForSeconds(1f);
        TimerTextChange(2);
        yield return new WaitForSeconds(1f);
        TimerTextChange(1);
        yield return new WaitForSeconds(1f);
        
        YandexGame.FullscreenShow(null, closeAd);
    }

    private void closeAd()
    {
        nowAdsShow = false;
        _timerPanel.SetActive(false);
    }

    private void TimerTextChange(int sec)
    {
        if (LocalizationManager.CurrentLanguage == "English")
            _timerText.text = _timerTextEn + ": " + sec.ToString();
        else if (LocalizationManager.CurrentLanguage == "Russian")
            _timerText.text = _timerTextRu + ": " + sec.ToString();
        else if (LocalizationManager.CurrentLanguage == "Turkish")
            _timerText.text = _timerTextTr + ": " + sec.ToString();
    }
}