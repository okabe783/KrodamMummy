using UnityEngine;

public class BasicEnemyTarget : MonoBehaviour, IEnemyTarget
{
    [SerializeField] float _targetRange = 10;
    [SerializeField] LayerMask _obstacle;
    [SerializeField] GameObject _targetMark;
    private Vector3 _playerPos;
    public void EnemyTarget()
    {
        Debug.Log("Basic Target Mode");
        _playerPos = TestPlayer.instance.Player.transform.position;
        var dis = Vector3.Distance(this.transform.position, _playerPos);
        if (dis > _targetRange)
            return;
        if (!Physics.SphereCast(this.transform.position, 3, _playerPos, out RaycastHit hit, _targetRange, _obstacle))
        {
            _targetMark.SetActive(true);
        }
    }
}