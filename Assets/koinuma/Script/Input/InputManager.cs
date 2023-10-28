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
            if (!_instance) //null�Ȃ�C���X�^���X������
            {
                var obj = new GameObject("InputManager");
                var input = obj.AddComponent<InputManager>();
                _instance = input;
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }
    /// <summary>�R�[���o�b�N�o�^�p</summary>
    GameInput _gameInput;
    /// <summary>�ړ��p�x�N�g��</summary>
    Vector2 _moveVector;
    /// <summary>�ړ��p�x�N�g���擾</summary>
    public Vector2 MoveVector { get => _moveVector; }

    /// <summary>InputType�Ƃ���ɑΉ�����Action��Dictionary</summary>
    Dictionary<InputType, Action> _inputDic = new Dictionary<InputType, Action>();

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        Initialization();
    }

    /// <summary>�������������s��</summary>
    void Initialization()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InputType)).Length; i++)
        {
            _inputDic.Add((InputType)i, null); // ������
        }

        // �R�[���o�b�N��o�^���Ă��� TODO:���삪�������ꍇ���������K�v������
        _gameInput.InGame.Move.started += OnMove;
        _gameInput.InGame.Move.performed += OnMove;
        _gameInput.InGame.Move.canceled += OnMove;
        _gameInput.InGame.Jump.started += OnJump;
        _gameInput.InGame.Attack.started += OnAttack;
    }

    #region �e�A�N�V�������Z�b�g
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
    
    /// <summary>�R�[���o�b�N�ɓo�^����Action���Z�b�g�o����</summary>
    /// <param name="inputType"></param><param name="action"></param>
    public void SetInput(InputType inputType, Action action)
    {
        _inputDic[inputType] += action;
    }
}

/// <summary>Input�̎��</summary>
public enum InputType
{
    Move,
    Jump,
    Attack
}