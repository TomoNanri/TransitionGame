using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateChild_Stable : AbstractStateChild
{
    private Coin _coin;
    private Color _originColor;
    private bool _isHit;
    public override void Initialize(int stateType)
    {
        base.Initialize(stateType);
        _coin = transform.parent.GetComponent<Coin>();
        _coin.HitAction += HitActionHnadler;
        _originColor = _coin.GetColor();
        Debug.Log($"[{name}] Coin color={_originColor}");
    }
    public override void OnEnter()
    {
        _coin.SetColor(_originColor);
        _isHit = false;
    }
    public override void OnExit()
    {
    }
    public override int StateUpdate()
    {
        if(_isHit)
        {
            _isHit = false;
            return (int)CoinStateController.StateType.FirstHit;
        }
        return StateType;
    }
    private void HitActionHnadler()
    {
        _isHit = true;
    }
}
