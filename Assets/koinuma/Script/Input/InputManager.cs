using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager _instance = default;
    public static InputManager Instance
    {
        get
        {
            if (!_instance) //nullならインスタンス化する
            {
                var obj = new GameObject("InputManager");
                var input = obj.AddComponent<InputManager>();
                _instance = input;
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    /// <summary>コールバック登録用</summary>
    GameInput _gameInput;
    /// <summary>移動用ベクトル</summary>
    Vector2 _moveVector;
    /// <summary>移動用ベクトル取得</summary>
    public Vector2 MoveVector { get => _moveVector; }

    /// <summary>InputTypeとそれに対応するActionのDictionary</summary>
    Dictionary<InputType, Action> _inputDic = new Dictionary<InputType, Action>();

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        Initialization();
    }

    /// <summary>初期化処理を行う</summary>
    void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _inputDic.Add((InputType)i, null); // 初期化
        }

        // コールバックを登録していく TODO:操作が増えた場合書き足す必要がある
        _gameInput.InGame.Move.started += OnMove;
        _gameInput.InGame.Move.performed += OnMove;
        _gameInput.InGame.Move.canceled += OnMove;
        _gameInput.InGame.Jump.started += OnJump;
        _gameInput.InGame.Attack.started += OnAttack;
    }

    #region 各アクションをセット
    void OnMove(InputAction.CallbackContext context)
    {
        _moveVector = context.ReadValue<Vector2>();
    }
    private void OnJump(InputAction.CallbackContext obj)
    {
        _inputDic[InputType.Jump]?.Invoke();
    }
    void OnAttack(InputAction.CallbackContext context)
    {
        _inputDic[InputType.Attack]?.Invoke();
    }
    #endregion
    
    /// <summary>コールバックに登録するActionをセット出来る</summary>
    /// <param name="inputType"></param><param name="action"></param>
    public void SetInput(InputType inputType, Action action)
    {
        _inputDic[inputType] += action;
    }
}

/// <summary>Inputの種類</summary>
public enum InputType
{
    Move,
    Jump,
    Attack
}