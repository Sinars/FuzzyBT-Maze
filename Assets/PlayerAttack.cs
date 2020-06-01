using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update


    public float attackRange;

    public String[] attackableEnemies;

    public void Attack()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
            attackRange, LayerMask.NameToLayer("Monster"))) return;
        Debug.Log("Hit something");
        Debug.Log("Hit " + hit.transform.tag);
        if (!attackableEnemies.Contains(hit.transform.parent.gameObject.tag)) return;
        Debug.Log("Hit monster");
        switch (hit.transform.gameObject.tag)
        {
            case Opponent.SPIDER:
                Debug.Log("Hit a spider");
                break;
            default:
                break;
        }

    }
}
