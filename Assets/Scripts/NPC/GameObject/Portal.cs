using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : BuildingBlock
{
    private void Start()
    {
        _flyweight = GetFlyweight(BlockCategory.BuildingBlock);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals(GameTag.Player)){
            collision.gameObject.GetComponent<PlayerBehaviour>().GoThroughPortal();
        }
    }
}
