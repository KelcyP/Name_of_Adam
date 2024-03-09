using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FallUnit : MonoBehaviour
{
    private static int _fallTypeCount = 4;
    private static Sprite[] _fallSprite;

    [SerializeField] private GameObject _fill;
    [SerializeField] private Image _fallImage;
    [SerializeField] private Animator _anim;

    private string _animName;
    private int _fallType = 0; // 0 = ȭ��Ʈ or ����, 1 = ����, 2 = �ݻ�

    private void InitSprite()
    {
        // �Ż� ���� �̹��� �ҷ����� (��, ��, ��)
        _fallSprite = new Sprite[_fallTypeCount];

        var allSprites = GameManager.Resource.LoadAll<Sprite>("Arts/UI/HP_Bar/FallGauge");
        int splitNum = allSprites.Length / _fallTypeCount;

        for (int i = 0; i < _fallTypeCount; i++)
            _fallSprite[i] = allSprites[i * splitNum];
    }

    public void InitFall(Team team, int fallType)
    {
        this._fallType = fallType;
        this._animName = $"Fall_Break_{team}_{fallType}";
        _anim.SetInteger("Type", fallType);
        _anim.SetBool("IsPlayer", team.Equals(Team.Player));

        SwitchCountImage(team);
    }

    public void SwitchCountImage(Team team)
    {
        if (_fallSprite == null)
        {
            // �̹��� ���� => �ҷ�����
            InitSprite();
        }

        if (team == Team.Player)
        {
            _fallImage.sprite = _fallSprite[0];
        }
        else
        {
            _fallImage.sprite = _fallSprite[_fallType];
        }
    }

    public void IncreaseGauge()
    {
        _anim.SetBool("IsPlay", true);
        _anim.Play(_animName, -1, 1);
        _anim.speed = -1;
    }

    public void DecreaseGauge()
    {
        _anim.SetBool("IsPlay", true);
        _anim.Play(_animName, -1, 0);
        _anim.speed = 1;
    }
}
