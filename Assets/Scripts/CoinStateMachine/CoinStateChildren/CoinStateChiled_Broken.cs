using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinStateChiled_Broken : AbstractStateChild
{
    private Coin _coin;
    private Color _originColor;
    public override void Initialize(int stateType)
    {
        base.Initialize(stateType);
        _coin = transform.parent.GetComponent<Coin>();
        _originColor = _coin.GetColor();
    }
    public override void OnEnter()
    {
        // 爆発エフェクト
        _coin.Explosion();
    }
    public override void OnExit()
    {
    }
    public override int StateUpdate()
    {
        return StateType;
    }
}
