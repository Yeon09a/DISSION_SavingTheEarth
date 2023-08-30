using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlowfish : Character
{
    public Transform target;

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    protected override void Update()
    {
        FollowTraget();
        base.Update();
    }

    private void FollowTraget()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

}
