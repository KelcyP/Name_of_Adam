using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarlotSceneController : MonoBehaviour
{
    private DeckUnit _stigmatizeUnit;

    [SerializeField] private Image _unitImage; // ���� �ο� ����


    void Start()
    {
        Init();
    }

    private void Init()
    {
        List<Script> scripts = new List<Script>();

        if (GameManager.Data.GameData.isVisitUpgrade == false)
            scripts = GameManager.Data.ScriptData["���μ�_����_����"];
        else
            scripts = GameManager.Data.ScriptData["���μ�_����"];

        GameManager.UI.ShowPopup<UI_Conversation>().Init(scripts);

        PassiveManager passiveManager = GameManager.Data.Passive;
    }

    public void OnStigmaUnitButtonClick()
    {
        GameManager.UI.ShowPopup<UI_MyDeck>("UI_MyDeck").Init(false, OnSelect);
    }

    public void OnSelect(DeckUnit unit)
    {
        _stigmatizeUnit = unit;
        _unitImage.sprite = unit.Data.Image;
        _unitImage.color = Color.white;

        GameManager.UI.ClosePopup();
        GameManager.UI.ClosePopup();
    }

    public void OnStigmaButtonClick()
    {
        if (_stigmatizeUnit != null)
        {
            GameManager.UI.ShowPopup<UI_StigmaSelectButtonPopup>().Init(_stigmatizeUnit, 3);
        }
    }

    public void OnStigmaSelect(Passive stigma)
    {
        _stigmatizeUnit.AddStigma(stigma);
        GameManager.UI.ClosePopup();
        AddStigamScript(stigma);
        //StartCoroutine(QuitScene());
        GameManager.Sound.Play("UI/UpgradeSFX/UpgradeSFX");
        //OnQuitClick();
    }

    public void AddStigamScript(Passive stigma)
    {
        UI_Conversation script = GameManager.UI.ShowPopup<UI_Conversation>();
        string scriptKey = "���μ�_" + stigma.GetName();
        script.Init(GameManager.Data.ScriptData[scriptKey], false);
        StartCoroutine(QuitScene(script));
    }

    public void OnQuitClick()
    {
        StartCoroutine(QuitScene());
    }

    private IEnumerator QuitScene(UI_Conversation eventScript = null)
    {
        if (GameManager.Data.GameData.isVisitStigma == false)
        {
            GameManager.Data.GameData.isVisitStigma = true;
        }

        if (eventScript != null)
            yield return StartCoroutine(eventScript.PrintScript());

        UI_Conversation quitScript = GameManager.UI.ShowPopup<UI_Conversation>();

        quitScript.Init(GameManager.Data.ScriptData["���μ�_����"], false);

        yield return StartCoroutine(quitScript.PrintScript());
        SceneChanger.SceneChange("StageSelectScene");
    }
}