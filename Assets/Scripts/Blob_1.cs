using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob_1 : Enemy
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Anim.SetTrigger("Die");
            EnemyState = State.Die;
        }
    }
}
