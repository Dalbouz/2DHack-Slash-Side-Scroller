using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{

    protected SpriteRenderer EnemySR;
    protected float StartHealth;
    protected float CurrentHealt;
    protected Animator Anim;
    protected Rigidbody2D Rb;
    protected PlayerMovement Player;
    public float MinDistance;
    public float RunSpeed = 3.0f;
    protected Vector2 DistanceToPlayer;
    private float _playerDistance;
    [SerializeField]
    private float _dieTime;
    private float _dieTimer;

    protected AnimatorClipInfo[] CurrentClipInfo;
    protected enum State
    {
        Idle,
        RunToIdlePoint,
        RunToPlayer,
        Die,
        Attack,
        ShotToPlayer,
        block
    }

    protected State EnemyState;

    public virtual void Start()
    {
        EnemyState = State.Idle;
        Anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        EnemySR = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

   public virtual void Update()
    {
        DistanceToPlayer = Player.transform.position - transform.position;
        _playerDistance = Mathf.Abs(DistanceToPlayer.x);
        switch (EnemyState)
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.RunToIdlePoint:
                //UpdateRunToIdlePoint();
                break;
            case State.RunToPlayer:
                UpdateRunToPlayer();
                break;
            case State.Die:
                UpdateDie();
                break;
            default:
                break;
        }
    }

    protected virtual void SetVelocityX(float x)
    {
        if(Player.transform.position.x < transform.position.x)
        {
            x *= -1;
        }
        Vector2 velocity = Rb.velocity;
        velocity.x = x;
        Rb.velocity = velocity;
    }
    public virtual void UpdateIdle()
    {
        SetVelocityX(0);
        if (_playerDistance < MinDistance)
        {

            Anim.SetTrigger("RunToPlayer");
            EnemyState = State.RunToPlayer;
        }
    }

    public virtual void UpdateRunToPlayer()
    {

        if (Player.transform.position.x < transform.position.x)
        {
            EnemySR.flipX = true;
        }
        else
        {
            EnemySR.flipX = false;
        }
        SetVelocityX(RunSpeed);
        //if (Player.transform.position.y >= transform.position.y)
        //{
        //    Anim.SetTrigger("GoToIdle");
        //    EnemyState = State.Idle;
        //}

    }
    public virtual void UpdateDie()
    {
        SetVelocityX(0);
        Rb.isKinematic = true;
        foreach (BoxCollider2D collider2D in GetComponents<BoxCollider2D>())
        {
            collider2D.enabled = false;
        }
          _dieTimer += Time.deltaTime;
        if(_dieTimer >= _dieTime)
        {
            
            gameObject.SetActive(false);
        }
    }


}
