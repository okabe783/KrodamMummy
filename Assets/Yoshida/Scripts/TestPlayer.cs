using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public static TestPlayer instance;

    private GameObject _player;

    public GameObject Player => _player;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {
        _player = this.gameObject;
    }
}