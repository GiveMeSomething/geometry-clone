using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class CustomObjectPool
{
    private IObjectPool<GameObject> _objectPool;

    private GameObject _targetPrefab;

    private bool collectionCheck = true;
    private int maxPoolSize = 10;

    public IObjectPool<GameObject> Pool
    {
        get
        {
            if(_objectPool == null)
            {
                _objectPool = new ObjectPool<GameObject>(
                    CreatePoolItem,
                    OnReturnedToPool,
                    OnTakeFromPool,
                    OnDestroyGameObject,
                    collectionCheck, maxPoolSize); ;
            }

            return _objectPool;
        }
    }

    public CustomObjectPool(GameObject prefab)
    {
        _targetPrefab = prefab;
    }

    private GameObject CreatePoolItem()
    {
        var newObject = GameObject.Instantiate(_targetPrefab);
        newObject.SetActive(false);

        return newObject;
    }

    private void OnReturnedToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void OnTakeFromPool(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void OnDestroyGameObject(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }
}

