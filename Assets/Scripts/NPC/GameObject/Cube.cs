using UnityEngine;
using System.Collections;
using System;

public class Cube : MonoBehaviour
{
	private IBlockFlyweight _flyweight;

	[SerializeField]
	private float _speed = GameConst.PLATFORM_SPEED;

	private Rigidbody2D _rigidbody;

	// Use this for initialization
	private void Start()
	{
		_flyweight = BuildingBlock.GetFlyweight(BlockCategory.BuildingBlock);
		
        //if (!TryGetComponent<Rigidbody2D>(out _rigidbody))
        //{
        //    throw new Exception("Cannot resolve component Rigidbody2D. Please check object/prefab");
        //}
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

