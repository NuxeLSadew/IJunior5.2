using System;
using UnityEngine;

public class Action : MonoBehaviour
{
    public float Range { get; protected set; }
    public TargetType TargetType { get; protected set; }

    public virtual Action Clone()
    {
        print("Ќельз€ создать клон базового действи€");
        throw new Exception();
    }

    public virtual void Do(ITargetable targetable)
    {
        throw new NotImplementedException();
    }
}
