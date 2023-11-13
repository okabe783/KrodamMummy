public class Enemy
{
    private IEnemyHitDamage _hitDamage;
    private IEnemyMove _move;
    // IEnemyTarget _target;

    public void SetHitDamage(IEnemyHitDamage hitDamage)
    {
        _hitDamage = hitDamage;
    }
    public void SetMove(IEnemyMove move)
    {
        _move = move;
    }
    public void SetTarget(IEnemyTarget target)
    {
        //_target = target;
    }
}