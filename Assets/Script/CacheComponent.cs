using System.Collections.Generic;
using UnityEngine;

public class CacheComponent
{
    private GameObject gameObject;
    private Dictionary<System.Type, Component> cache = new Dictionary<System.Type, Component>();
    private Dictionary<string, object> cacheVariable = new();


    public CacheComponent(GameObject obj)
    {
        gameObject = obj;
    }

    public T GetComponent<T>() where T : Component
    {
        System.Type type = typeof(T);

        if (cache.ContainsKey(type))
        {
            return cache[type] as T;
        }

        T component = gameObject.GetComponent<T>();
        if (component != null)
        {
            cache[type] = component;
        }

        return component;
    }
    public void SetValue<Y>(string name, Y value)
    {
        cacheVariable[name] = value;

    }
    public Y GetValue<Y>(string name)
    {
        if (cacheVariable.ContainsKey(name))
        {
            return (Y)cacheVariable[name];
        }
        else
        {
            return default;
        }
    }
}