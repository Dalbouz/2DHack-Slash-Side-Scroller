using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackManager : MonoBehaviour
{
    public static AttackManager instance;

    public bool CanReceiveInput = true;
    public bool InputReceived = false;
    public bool IsAttacking = false;
    private void Awake()
    {
        if (gameObject.activeInHierarchy) instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void Attack()
    {
            if (CanReceiveInput)
            {
                InputReceived = true;
                CanReceiveInput = false;
            }
            else
            {
                return;
            }

    }
    public void InputManager()
    {
        if (!CanReceiveInput)
        {
            CanReceiveInput = true;
        }
        else
        {
            CanReceiveInput = false;
        }
    }
}
