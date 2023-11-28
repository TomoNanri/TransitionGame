using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleA : Product
{
    public override string Description { get; set; }
    public ObstacleA(string description)
    {
        Description = description;
    }
}
