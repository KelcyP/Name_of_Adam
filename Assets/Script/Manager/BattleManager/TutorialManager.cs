using Newtonsoft.Json.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    /// <summary>
    /// ������ ��µ� ���ڿ�
    /// [CTRL]�� ������ ���ڿ��� ������ ���� �׼��� �ϴ� �ܰ踦 �ǹ�
    /// ��, ������ Ư�� �ൿ���� ���� Ʃ�丮�� ���� ����
    /// </summary>
    private readonly string[] TooltipTexts =
    {
        // Ʃ�丮�� 1 ����
        "<color=#FF9696>�÷��̾� ��<color=white>���� ������ ��ȯ�ϰų� ��ų�� �� �� �ֽ��ϴ�.",
        "������ ��ȯ�ϰų� ��ų�� ����Ҷ� �ʿ��� <color=#FF9696>����<color=white>�Դϴ�.\n�÷��̾� ���� �� ������ <color=#FF9696>30<color=white>�� ȸ���մϴ�.",
        "���� ��ȯ�� �� �ִ� ���ֵ��Դϴ�.\n<color=#FF9696>ù��° �÷��̾� ��<color=white>���� <color=#FF9696>������ ����<color=white>�� ����Ͽ� ������ ��ȯ�� �� �ֽ��ϴ�.",
        "������ �����ϴ� ��ų���Դϴ�.",
        "�����⸦ ��ȯ�غ�����.[CTRL]",
        "�����⸦ ��ȯ�غ�����.[CTRL]",
        "���� �����ϸ� <color=#FF9696>���� ��<color=white>���� �Ѿ�ϴ�.[CTRL]",
        "<color=#FF9696>���� ��<color=white>���� �ʵ忡 �ִ� �� ���ֵ��� �ӵ��� ���� �����Դϴ�.\n������ <color=#FF9696>�ӵ�ǥ<color=white>���� ��ܿ� �ִ� �����ϼ��� ���� �ൿ�մϴ�.",
        "�� ������ ��ĭ �̵� �� ���� ������ �� �ֽ��ϴ�.\n�����⸦ ������ ��ĭ �̵����Ѻ�����.[CTRL]",
        "�˺��� �����غ�����.[CTRL]",

        // Ʃ�丮�� 2 ����
        "Ư�� ������ ����ϰų� ��ų�� ����� �� �ʿ��� <color=#FF9696>���� ����<color=white>�Դϴ�.\n���� óġ�� ������ �ϳ��� ���� �� �ֽ��ϴ�.",
        "����� ���� <color=#FF9696>Ÿ��<color=white>�ϴµ� ������ ������ ���� ���� �����̸� �����Ӹ� �ƴ϶� <color=#FF9696>���� ����<color=white>���� �Ҹ��մϴ�.\n���縦 �����ϼ���.[CTRL]",
        "����� ���� <color=#FF9696>Ÿ��<color=white>�ϴµ� ������ ������ ���� ���� �����̸� �����Ӹ� �ƴ϶� <color=#FF9696>���� ����<color=white>���� �Ҹ��մϴ�.\n���縦 �����ϼ���.[CTRL]",
        "���� �� ���� <color=#FF9696>�ž�<color=white>�� ����߸��� <color=#FF9696>�Ǽ� ����<color=white>�Դϴ�.\n����� �� �Ǽ� ������ <color=#FF9696>2ȸ<color=white> ��� ������ ������ �ֽ��ϴ�.\n�� Ȱ���Ͽ� ���� Ÿ�����Ѻ�����.",
        "<color=#FF9696>�� ���� ��ư<color=white>�� ���� ���� ������ �Ѿ����.[CTRL]",
        "�̵��� �ʿ䰡 ���� ��� <color=#FF9696>�� ���� ��ư<color=white>�� ���� ���� �ѱ� �� �־��.[CTRL]",
        "�˺��� �����Ͽ� <color=#FF9696>�ž�<color=white>�� ����߸�����.[CTRL]",
        "<color=#FF9696>�ž�<color=white>�� ����߸��� ��ų <color=#FF9696>�ӻ���<color=white>�� ����Ͽ� ���� Ÿ������ ������.[CTRL]",
        "<color=#FF9696>�ž�<color=white>�� ����߸��� ��ų <color=#FF9696>�ӻ���<color=white>�� ����Ͽ� ���� Ÿ������ ������.[CTRL]",
        "���� Ÿ����ų ��� �ش� ���� �Ʊ����� ����� <color=#FF9696>����<color=white>�� �ο��� �� �ֽ��ϴ�.\n�˺����� �ο��� ������ �����ϼ���.[CTRL]",
        "���� �˺��� ����� ������ �Ǿ����ϴ�.\n���� �� ���Ḧ ��������.[CTRL]",
        "�Ʊ��� �̹� �ִ� ��ġ�� �̵��� �� �� ������ ���� ��ġ�� �ٲߴϴ�.\n���縦 �̵���Ű����.[CTRL]",
        "���ฦ �����Ͽ� ���� ������ ���ֺ�����.[CTRL]",
        "�˺��� �̵���Ű����.[CTRL]",
        "���� ������ ����� ���ฦ �������ϼ���.[CTRL]",
        "",
    };

    public const int STEP_BOUNDARY = 100;

    private const float RECLICK_TIME = 0.5f;

    private static TutorialManager _instance;
    public static TutorialManager Instance
    {
        set
        {
            if (_instance == null)
                _instance = value;
        }
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private UI_Tutorial UI;

    [SerializeField]
    private TutorialStep _step;

    public TutorialStep Step => _step;

    private TooltipData currentTooltip;

    public bool IsTutorialactive;
    private bool isEnable;
    private bool isCanClick;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int curID = GameManager.Data.Map.CurrentTileID;

        switch (curID)
        {
            case 1: _step = TutorialStep.UI_PlayerTurn; break;
            case 2: _step = TutorialStep.UI_FallSystem; break;
            case 3: _step = TutorialStep.UI_UnitDead; break;
        }

        isCanClick = true;
    }

    private void Update()
    {
        if (!isEnable)
            return;

        if (UI.ValidToPassTooltip)
        {
            if (isCanClick && GameManager.InputManager.Click)
            {
                StartCoroutine(ClickCoolTime());
                ShowNextTutorial();
            }
        }
    }

    public void ShowNextTutorial()
    {
        if (CheckStep(TutorialStep.UI_Defeat) || CheckStep(TutorialStep.UI_Last))
            return; // ������ UI Ʃ�丮�� ���� Step�� ���Ǻ� �����̱� ������ ���� ó��

        SetNextStep();
        ShowTutorial();
    }

    public void ShowPreviousTutorial()
    {
        SetPreviousStep();
        ShowTutorial();
    }

    public bool IsEnable()
        => !GameManager.OutGameData.isTutorialClear() && isEnable;

    public void SetNextStep()
    {
        TutorialStep[] steps = (TutorialStep[])Enum.GetValues(typeof(TutorialStep));
        int next = Array.IndexOf(steps, _step) + 1;
        _step = (steps.Length == next) ? steps[0] : steps[next];
    }

    private void SetPreviousStep()
    {
        TutorialStep[] steps = (TutorialStep[])Enum.GetValues(typeof(TutorialStep));
        int next = Array.IndexOf(steps, _step) - 1;
        _step = (steps.Length == -1) ? steps[steps.Length - 1] : steps[next];
    }

    private bool IsToolTip(TutorialStep step)
        => (int)step % STEP_BOUNDARY != 0;

    private TooltipData AnalyzeTooltip(TutorialStep step)
    {
        TooltipData tooltip = new TooltipData();
        int indexToTooltip = (int)step % STEP_BOUNDARY - 1;

        tooltip.Step = step;
        tooltip.Info = TooltipTexts[indexToTooltip].Replace("[CTRL]", "");
        tooltip.IndexToTooltip = indexToTooltip;
        tooltip.IsCtrl = TooltipTexts[indexToTooltip].Contains("[CTRL]");
        tooltip.IsEnd = false;

        if (CheckStep(TutorialStep.Tutorial_End_1) || 
            CheckStep(TutorialStep.Tutorial_End_2) || 
            CheckStep(TutorialStep.Tutorial_End_3))
            tooltip.IsEnd = true;

        return tooltip;
    }

    private int AnalyzeUI(TutorialStep step) => (int)step / STEP_BOUNDARY - 1;

    public bool CheckStep(TutorialStep step) => this.Step == step;

    public void ShowTutorial()
    {
        if (IsToolTip(_step))
        {
            // Tooltip ���
            currentTooltip = AnalyzeTooltip(_step);
            if (currentTooltip.IsEnd)
                DisableToolTip();
            else
                EnableToolTip(currentTooltip);
        }
        else
        {
            // UI ���
            int indexToUI = AnalyzeUI(_step);
            isEnable = true;
            UI.TutorialActive(indexToUI);
        }
    }

    public void DisableToolTip()
    {
        UI.CloseToolTip();
        UI.SetUIMask(-1);
        UI.SetValidToPassToolTip(false);
        SetActiveAllTiles(true);
        isEnable = false;
    }

    public void EnableToolTip(TooltipData data)
    {
        UI.ShowTooltip(data.Info, data.IndexToTooltip);
        UI.SetUIMask(data.IndexToTooltip);
        UI.SetValidToPassToolTip(!data.IsCtrl);
        SetTutorialField(data.Step);
        isEnable = true;
    }

    private void SetTutorialField(TutorialStep step)
    {
        SetActiveAllTiles(false);

        switch (step)
        {
            case TutorialStep.Tooltip_UnitSpawnSelect:
                BattleManager.Field.TileDict[new Vector2(1, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitMove:
                BattleManager.Field.TileDict[new Vector2(2, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_BlackKnightSpawn:
                BattleManager.Field.TileDict[new Vector2(2, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack_2:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_PlayerSkillUse:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitSwap:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack_3:
                BattleManager.Field.TileDict[new Vector2(4, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitSwap_2:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack_4:
                BattleManager.Field.TileDict[new Vector2(4, 1)].SetActiveCollider(true);
                break;
        }
    }

    private void SetActiveAllTiles(bool isActive)
    {
        foreach (Tile tile in BattleManager.Field.TileDict.Values)
            tile.SetActiveCollider(isActive);
    }

    private IEnumerator ClickCoolTime()
    {
        isCanClick = false;
        yield return new WaitForSeconds(RECLICK_TIME);
        isCanClick = true;
    }
}