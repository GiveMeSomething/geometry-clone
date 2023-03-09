using UnityEngine;
using System;
using System.Collections;

public class Slab : MonoBehaviour
{
    private IBlockFlyweight _flyweight;

    [SerializeField]
    private float _speed = GameConst.PLATFORM_SPEED;

    private Rigidbody2D _rigidbody;

    // Use this for initialization
    private void Start()
    {
        BlockFlyweightFactory flyweightFactory = BlockFlyweightFactory.GetInstance();
        _flyweight = flyweightFactory.GetFlyweight(this.GetType());

        _rigidbody = GetComponent<Rigidbody2D>();

        if(_flyweight == null)
        {
            throw new Exception($"Cannot resolve flyweight type for {this.GetType().ToString()}");
        }

        if(_rigidbody == null)
        {
            throw new Exception("Cannot resolve component Rigidbody2D. Please check object/prefab");
        } 
    }

    // Update is called once per frame
    private void Update()
    {
        _flyweight.Move(transform, _speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _flyweight.HandleCollision(collision);
    }
}

