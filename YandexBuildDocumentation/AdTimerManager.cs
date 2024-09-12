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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(180f);

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
        _timerPanel.SetActive(false);
        StartCoroutine(Timer());
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
