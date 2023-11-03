using System;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    static InGameManager _instance;
    public static InGameManager Instance { get => _instance; }
    public event Action OnUpdateAction;

    private void Awake()
    {
        if (_instance) Destroy(this.gameObject);
        else _instance = this;
    }

    private void Update()
    {
        OnUpdateAction?.Invoke();
    }

    private void OnDisable()
    {
        OnUpdateAction = null;
    }
}