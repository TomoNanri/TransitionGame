using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateChild_FirstHit : AbstractStateChild
{
    private Coin _coin;
    private bool _isHit;
    private float _timer;
    public override void Initialize(int stateType)
    {
        base.Initialize(stateType);
        _coin = transform.parent.GetComponent<Coin>();
        _coin.HitAction += HitActionHnadler;
    }
    public override void OnEnter()
    {
        _timer = 30f;
        _coin.SetColor(new Color(255, 0, 0));
        _isHit = false;
    }
    public override void OnExit()
    {

    }
    public override int StateUpdate()
    {
        _timer -= Time.deltaTime;
        if(_timer < 0)
        {
            return (int)CoinStateController.StateType.Stable;
        }
        if(_isHit )
        {
            return (int)CoinStateController.StateType.SecondHit;
        }
        return StateType;
    }
    private void HitActionHnadler()
    {
        _isHit = true;
    }
}
