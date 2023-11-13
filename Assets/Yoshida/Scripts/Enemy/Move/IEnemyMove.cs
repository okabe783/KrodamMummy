/// <summary>
/// Enemyの移動管理インターフェース
/// </summary>
public interface IEnemyMove
{
    /// <summary>
    /// Enemyの移動管理メソッド
    /// </summary>
    /// <param name="speed">移動速度</param>
    public void EnemyMove(int speed);
}