public abstract class EnemyTypes
{
    public abstract    float                     Health       { get; }
    public abstract    EnemyController.EnemyType Type         { get; }
    protected abstract int                       AttackDamage { get; }
    public abstract    float                     AttackSpeed  { get; }
    public abstract    float                     Speed        { get; }
    public abstract    int                       Score        { get; }

    public virtual void Attack(HealthManager _healthManager)
    {
        _healthManager.TakeDamage(AttackDamage);
    }
}

public class NormalEnemy : EnemyTypes
{
    public override float Health => 12;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.NORMAL;

    protected override int   AttackDamage => 5;
    public override    float AttackSpeed  => 1.0f;
    public override    float Speed        => 2.0f;
    public override    int   Score        => 10;
}

public class FastEnemy : EnemyTypes
{
    public override float Health => 5;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.FAST;

    protected override int   AttackDamage => 2;
    public override    float AttackSpeed  => 1.0f;
    public override    float Speed        => 3.0f;
    public override    int   Score        => 5;
}

public class TankEnemy : EnemyTypes
{
    public override float Health => 20;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.TANK;

    protected override int   AttackDamage => 10;
    public override    float AttackSpeed  => 1.0f;
    public override    float Speed        => 1.5f;
    public override    int   Score        => 20;
}

public class DaggerEnemy : EnemyTypes
{
    public override float Health => 5;

    public override EnemyController.EnemyType Type => EnemyController.EnemyType.DAGGER;

    protected override int   AttackDamage => 1;
    public override    float AttackSpeed  => 1.0f;
    public override    float Speed        => 4.0f;
    public override    int   Score        => 5;

    private bool _isFirstAttack = true;

    public override void Attack(HealthManager _healthManager)
    {
        if (_isFirstAttack)
        {
            _isFirstAttack = false;
            _healthManager.TakeDamage(15);
            return;
        }

        _healthManager.TakeDamage(AttackDamage);
    }
}