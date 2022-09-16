using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviourPunCallbacks where T : MonoBehaviourPunCallbacks
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject go = new GameObject($"@{nameof(T)}");
                    _instance = go.AddComponent<T>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance != null)
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }

            return;
        }

        _instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
    }
}
