public abstract class EnemyTypes
{
    public abstract float                     Health       { get; }
    public abstract EnemyController.EnemyType Type         { get; }
    public abstract int                       AttackDamage { get; }
    public abstract float                     AttackSpeed  { get; }
    public abstract float                     Speed        { get; }

    public virtual void Attack(HealthManager _healthManager)
    {
        _healthManager.TakeDamage(AttackDamage);
    }
}

public class NormalEnemy : EnemyTypes
{
    public override float Health => 10;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.NORMAL;

    public override int   AttackDamage => 5;
    public override float AttackSpeed  => 1.0f;
    public override float Speed        => 2.0f;
}

public class FastEnemy : EnemyTypes
{
    public override float Health => 5;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.FAST;

    public override int   AttackDamage => 2;
    public override float AttackSpeed  => 1.0f;
    public override float Speed        => 3.0f;
}

public class TankEnemy : EnemyTypes
{
    public override float Health => 20;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.TANK;

    public override int   AttackDamage => 10;
    public override float AttackSpeed  => 1.0f;
    public override float Speed        => 1.0f;
}

public class DaggerEnemy : EnemyTypes
{
    public override float Health => 5;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.DAGGER;

    public override int   AttackDamage => 1;
    public override float AttackSpeed  => 1.0f;
    public override float Speed        => 4.0f;

    private bool _isFirstAttack = true;

    public override void Attack(HealthManager _healthManager)
    {
        if (_isFirstAttack)
        {
            _isFirstAttack = false;
            _healthManager.TakeDamage(AttackDamage * 3);
            return;
        }

        _healthManager.TakeDamage(AttackDamage);
    }
}