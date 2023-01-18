using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDataManager
{
    #region FieldMNG
    private FieldManager _FieldMNG;
    public FieldManager FieldMNG => _FieldMNG;
    #endregion
    #region BattleUnitMNG
    private BattleUnitManager _BattleUnitManager;
    public BattleUnitManager BattleUnitMNG => _BattleUnitManager;
    #endregion
    #region DataMNG
    private DataManager _DataMNG;
    public DataManager DataMNG => _DataMNG;
    #endregion

    // 필드와 배틀유닛, 마나의 정보 및 관리는 각 매니저로 분할

    public BattleDataManager()
    {
        _FieldMNG = new FieldManager();
        _BattleUnitManager = new BattleUnitManager();
        _DataMNG = GameManager.Instance.DataMNG;
    }
     
    #region Turn
    private int _Turn;
    public int Turn => _Turn;

    public void TurnPlus()
    {
        _Turn++;
    }
    #endregion

    #region Prepare / Engage Stage
    private bool _EngageStage;
    public bool EngageStage => _EngageStage;
    //즉 현재 상태가 전투 단계면 참이니
    //거짓을 반환시 준비 단계이다.
    public void SetEngageStage(bool stage)
    {
        _EngageStage = stage;
    }
    #endregion

    #region DeckUnitList
    private List<DeckUnit> _DeckUnitList = new List<DeckUnit>();
    public List<DeckUnit> DeckUnitList => _DeckUnitList;

    public void CopyDeckList(DeckUnit unit) {
        foreach (DeckUnit d in DataMNG.DeckUnitList)
        {
            AddDeckUnit(d);
        }
    }

    public void AddDeckUnit(DeckUnit unit) {
        DeckUnitList.Add(unit);
    }

    public void RemoveDeckUnit(DeckUnit unit) {
        DeckUnitList.Remove(unit);
    }

    public DeckUnit GetRandomDeckUnit() {
        if (DeckUnitList.Count == 0)
        {
            return null;
        }
        int randNum = Random.Range(0, DeckUnitList.Count);
        
        DeckUnit unit = DeckUnitList[randNum];
        _DeckUnitList.RemoveAt(randNum);

        return unit;
    }
    
    #endregion

    #region Mana

    #region ManaCost
    private int _ManaCost = 0;
    public int ManaCost => _ManaCost;
    #endregion

    ManaGuage _manaGuage;

        public void InitMana()
    {
        _ManaCost = 0;
    }

    public void SetManaGuage(ManaGuage _guage)
    {
        _manaGuage = _guage;
    }

    public void ChangeMana(int value)
    {
        _ManaCost += value;

        if (10 <= _ManaCost)
            _ManaCost = 10;
        else if (_ManaCost < 0)
            _ManaCost = 0;
        
        _manaGuage.DrawGauge();
    }

    public bool CanUseMana(int value)
    {
        if (_ManaCost >= value)
            return true;
        else
            return false;
    }
    #endregion
}
