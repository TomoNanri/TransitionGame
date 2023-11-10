using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStateController : AbstractStateController
{
    public enum StateType
    {
        Stable,
        FirstHit,
        SecondHit,
        Broken
    }
    public override void Initialize(int initializeStateType)
    {
        stateDic[(int)StateType.Stable] = gameObject.AddComponent<CoinStateChild_Stable>();
        stateDic[(int)StateType.Stable].Initialize((int)StateType.Stable);
        stateDic[(int)StateType.FirstHit] = gameObject.AddComponent<CoinStateChild_FirstHit>();
        stateDic[(int)StateType.FirstHit].Initialize((int)StateType.FirstHit);
        stateDic[(int)StateType.SecondHit] = gameObject.AddComponent<CoinStateChild_SecondHit>();
        stateDic[(int)StateType.SecondHit].Initialize((int)StateType.SecondHit);
        stateDic[(int)StateType.Broken] = gameObject.AddComponent<CoinStateChiled_Broken>();
        stateDic[(int)StateType.Broken].Initialize((int)StateType.Broken);
        CurrentState = initializeStateType;
        stateDic[CurrentState].OnEnter();
    }
}
