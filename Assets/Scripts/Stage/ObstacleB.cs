using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleB : Product
{
    public override string Description { get; set; }
    public ObstacleB(string description)
    {
        Description = description;
    }
}
