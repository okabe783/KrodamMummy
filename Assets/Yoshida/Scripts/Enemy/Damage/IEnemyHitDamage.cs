/// <summary>
/// Enemyの被ダメージ処理インターフェース
/// </summary>
public interface IEnemyHitDamage
{
    /// <summary>
    /// Enemyの被ダメージ処理メソッド
    /// </summary>
    /// <param name="damage">被ダメージ</param>
    public void EnemyHitDamage(int damage);
}