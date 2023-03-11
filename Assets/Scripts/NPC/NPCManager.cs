using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _cubePrefab;

	[SerializeField]
	private GameObject _slabPrefab;

	[SerializeField]
	private GameObject _spikePrefab;

	private Dictionary<BlockType, CustomObjectPool> _npcPoolMap;

    private void Start()
    {
		_npcPoolMap = new();
    }

	public CustomObjectPool GetObjectPool(BlockType blockType)
	{
		if(_npcPoolMap.TryGetValue(blockType, out var objectPool))
		{
			return objectPool;
		}

		// Only instantiate object pool of a type when it is needed
		switch(blockType)
		{
			case BlockType.Cube:
				objectPool = new(_cubePrefab);
				break;
			case BlockType.Slab:
                objectPool = new(_slabPrefab);
                break;
			case BlockType.Spike:
				objectPool = new(_spikePrefab);
				break;
        }

		_npcPoolMap.Add(blockType, objectPool);
		return objectPool;
	}
}

