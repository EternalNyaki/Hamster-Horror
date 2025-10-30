using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //Public property for accessing the singleton instance
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                //Set the instance
                _instance = FindAnyObjectByType<T>();
                _instance.Initialize();
            }
            return _instance;
        }
    }

    //The singleton instance itself
    private static T _instance;

    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
        {
            //Set the instance
            _instance = (T)this;
            Initialize();
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"Cannot have multiple {GetType().Name} objects in one scene. Destroying the newest instance");
            Destroy(this);
        }
    }

    ///<summary>
    ///Override for initialization instead of using Start() or Awake()
    ///</summary>
    protected virtual void Initialize() { }
}
