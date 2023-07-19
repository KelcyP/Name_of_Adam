using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnData
{
    public GameObject prefab;
    public DeckUnit deckUnit;
    public Vector2 location;
    public Team team;
    public Passive[] stigmas;
}

// 핸드 -> 필드 위에 생성
// 데이터 -> 필드 생성
public class UnitSpawner : MonoBehaviour
{
    private Transform parent;

    // 디버그용
    [SerializeField] List<SpawnData> AnimTest;
    // 디버그용

    private void Awake()
    {
        parent = SetParent();
    }
    private void Start()
    {
        // 디버그용
        if (GameManager.Data.CurrentStageData.Units.Count == 0)
        {
            foreach (SpawnData data in AnimTest)
              InitSpawn(data);
        }
        // 디버그용
    }

    private void InitSpawn(SpawnData spawndata)
    {
        if (BattleManager.Field.TileDict[spawndata.location].UnitExist)
        {
            Debug.Log("해당 타일에 유닛이 존재합니다.");
        }
        else
        {
            GameObject go = GameObject.Instantiate(spawndata.prefab, parent);
            BattleUnit unit = go.GetComponent<BattleUnit>();

            unit.Init(spawndata.location, Team.Enemy);
        }
    }

    public BattleUnit DeckSpawn(DeckUnit deckUnit, Vector2 location)
    {
        GameObject go = GameManager.Resource.Instantiate("BattleUnits/BattleUnit", parent);
        BattleUnit unit = go.GetComponent<BattleUnit>();
        unit.DeckUnit = deckUnit;

        unit.Init(location, Team.Player);
        
        return unit;
    }

    public void SpawnInitialUnit()
    {
        List<StageUnitData> datas = GameManager.Data.CurrentStageData.Units;
        string factionName = GameManager.Data.CurrentStageData.FactionName;

        foreach (StageUnitData data in datas)
        {
            SpawnData sd = new SpawnData();
            sd.prefab = GameManager.Resource.Load<GameObject>($"Prefabs/BattleUnits/{factionName}/{data.Name}");
            sd.location = data.Location;
            sd.team = Team.Enemy;

            //sd.deckUnit.Data = sd.prefab.GetComponent<BattleUnit>()
            InitSpawn(sd);

            //Spawn(data, data.location);
            //SpawnTest(data, data.Location);
        }
        //foreach (SpawnData data in SpawnMonsters)
        //{
        //    Spawn(data, data.location);
        //}
    }

    private Transform SetParent()
    {
        GameObject go = GameObject.Find("Units");
        if (go == null)
            go = new GameObject("Units");
        return go.transform;
    }
}
