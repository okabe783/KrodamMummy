using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerState _playerState;
    public PlayerState PlayerState { get => _playerState; set => _playerState = value; }
}

public enum PlayerState
{
    nomal,
    air,
    canneling
}