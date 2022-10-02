using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// ����� ������ � ��������� �������.
/// ����������� ����� ������� ���� Dictionary.text
/// � �����  Assets/Resources/Data/ ����� ��������������
/// </summary>
public class Translater : MonoBehaviour, ITranslator
{
    public static Translater Instance;
    [SerializeField] private string[] DictionaryLanguages;
    [SerializeField] private string[] DictionaryWordsToTranslate;
    [SerializeField] private int wordIndex = -1;
    [SerializeField] private Language m_Language = Language.EN;
    [SerializeField] private List<Text> translateObjects = new List<Text>();

    private enum Language
    {
        RU,
        EN
    };

    private void Awake() => Instance = this;

    private void Start() => LoadLanguage();

    private void ChangeLanguage()
    {
        translateObjects[0].text = Translater.Instance.GetTranslate("������ 100 ����� �� �������!");
        translateObjects[1].text = Translater.Instance.GetTranslate("��������� ���� ������?");
        translateObjects[2].text = Translater.Instance.GetTranslate("������� 80 �����������?");
        translateObjects[3].text = Translater.Instance.GetTranslate("������� �������� 150 �����?");
        translateObjects[4].text = Translater.Instance.GetTranslate("������������ � ���� 3 ������?");
        translateObjects[5].text = Translater.Instance.GetTranslate("�� ������ ������� ����?");
        translateObjects[6].text = Translater.Instance.GetTranslate("���");
        translateObjects[7].text = Translater.Instance.GetTranslate("�����");
        translateObjects[8].text = Translater.Instance.GetTranslate("��");
        translateObjects[9].text = Translater.Instance.GetTranslate("���� ��������");
        translateObjects[10].text = Translater.Instance.GetTranslate("������� ���� �� ������...");
        translateObjects[11].text = Translater.Instance.GetTranslate("������� �� ���������� �� �������� �������?");
        translateObjects[12].text = Translater.Instance.GetTranslate("���");
        translateObjects[13].text = Translater.Instance.GetTranslate("��");
        if (Parser.IntParsing(translateObjects[14].text) == 0)
            translateObjects[14].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[15].text) == 0)
            translateObjects[15].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[16].text) == 0)
            translateObjects[16].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[17].text) == 0)
            translateObjects[17].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[18].text) == 0)
            translateObjects[18].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[19].text) == 0)
            translateObjects[19].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[20].text) == 0)
            translateObjects[20].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[21].text) == 0)
            translateObjects[21].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[22].text) == 0)
            translateObjects[22].text = Translater.Instance.GetTranslate("�������");
        if (Parser.IntParsing(translateObjects[23].text) == 0)
            translateObjects[23].text = Translater.Instance.GetTranslate("�������");
    }

    private void LoadLanguage()
    {
        string langCode = PlayerPrefs.GetString("CurrentLanguage", "EN");
        GameManager.instance.SetCheckmarksOnLanguage(langCode);
        SetLanguageLocal(langCode);
    }

    public string GetTranslate(string text, Text translateObject)
    {
        translateObjects.Add(translateObject);

        return Translate(text);
    }

    public string GetTranslate(string text)
    {
        return Translate(text);
    }

    private string Translate(string text)
    {
        switch (m_Language)
        {
            case Language.EN:
                foreach (string DictionaryLanguage in DictionaryLanguages)
                {
                    string[] DictionaryWords = DictionaryLanguage.Split('_');
                    for (int i = 0; i < DictionaryWords.Length; i++)
                    {
                        if (text == DictionaryWords[i])
                        {
                            wordIndex = i;
                            break;
                        }
                    }

                    if (wordIndex != -1)
                        break;
                }

                DictionaryWordsToTranslate = DictionaryLanguages[1].Split('_');
                text = DictionaryWordsToTranslate[wordIndex];
                break;

            case Language.RU:
                foreach (string DictionaryLanguage in DictionaryLanguages)
                {
                    string[] DictionaryWords = DictionaryLanguage.Split('_');
                    for (int i = 0; i < DictionaryWords.Length; i++)
                    {
                        if (text == DictionaryWords[i])
                        {
                            wordIndex = i;
                            break;
                        }
                    }

                    if (wordIndex != -1)
                        break;
                }

                DictionaryWordsToTranslate = DictionaryLanguages[0].Split('_');
                text = DictionaryWordsToTranslate[wordIndex];
                break;
        }

        return text;
    }

    public void SetLanguage(string langCode)
    {
        switch (langCode)
        {
            case "EN":
                m_Language = Language.EN;
                PlayerPrefs.SetString("CurrentLanguage", "EN");
                break;

            case "RU":
                m_Language = Language.RU;
                PlayerPrefs.SetString("CurrentLanguage", "RU");
                break;
        }
        ChangeLanguage();
        PlayerPrefs.Save();
    }

    private void SetLanguageLocal(string langCode)
    {
        switch (langCode)
        {
            case "EN":
                m_Language = Language.EN;
                break;
            case "RU":
                m_Language = Language.RU;
                break;
        }
        ChangeLanguage();
    }
    public void DictionaryLanguagesSplit()
    {
        #if UNITY_EDITOR
        DictionaryLanguages = FileManager.ReadFile("C:/Users/Kenny McCormic/Desktop/GameTemplates/aMoonCat-main/Assets/MoonCatResources/Data/Dictionary.txt").Split('/');
        #endif
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(Translater))]
public class TranslaterEditor : Editor
{
    public Translater translater;

    private void OnEnable()
    {
        translater = (Translater)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Dictionary Languages Split"))
            translater.DictionaryLanguagesSplit();
    }
}
#endif