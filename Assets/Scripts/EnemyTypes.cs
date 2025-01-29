using System.Collections.Generic;

public abstract class EnemyTypes
{
    public abstract float                     Health       { get; set; }
    public abstract EnemyController.EnemyType Type         { get; set; }
    public abstract int                       AttackDamage { get; set; }
    public abstract float                     AttackSpeed  { get; set; }
    public abstract float                     Speed        { get; set; }
    public abstract int                       Score        { get; set; }
    public abstract Dictionary<string, float> Modifiers    { get; }

    public virtual void Attack(HealthManager _healthManager)
    {
        _healthManager.TakeDamage(AttackDamage);
    }
}

public class NormalEnemy : EnemyTypes
{
    public sealed override EnemyController.EnemyType Type { get; set; } = EnemyController.EnemyType.NORMAL;

    public sealed override int Score { get; set; } = 10;

    public sealed override float Speed { get; set; } = 2.0f;

    public sealed override float AttackSpeed { get; set; } = 1.0f;

    public sealed override int AttackDamage { get; set; } = 5;

    public sealed override float Health { get; set; } = 12;

    public override Dictionary<string, float> Modifiers => new()
    {
        { "Health", 1.0f },
        { "AttackDamage", 1.0f },
        { "AttackSpeed", 1.0f },
        { "Speed", 1.0f },
        { "Score", 1.0f }
    };
}

public class FastEnemy : EnemyTypes
{
    public sealed override float Health { get; set; } = 5;

    public sealed override EnemyController.EnemyType Type { get; set; } = EnemyController.EnemyType.FAST;

    public sealed override int   AttackDamage { get; set; } = 2;
    public sealed override    float AttackSpeed  { get; set; } = 1.0f;
    public sealed override    float Speed        { get; set; } = 3.0f;
    public sealed override    int   Score        { get; set; } = 5;

    public override Dictionary<string, float> Modifiers => new()
    {
        { "Health", 0.5f },
        { "AttackDamage", 0.5f },
        { "AttackSpeed", 1.0f },
        { "Speed", 1.5f },
        { "Score", 0.5f }
    };
}

public class TankEnemy : EnemyTypes
{
    public sealed override float Health { get; set; } = 20;

    public sealed override EnemyController.EnemyType Type { get; set; } = EnemyController.EnemyType.TANK;

    public sealed override int   AttackDamage { get; set; } = 10;
    public sealed override    float AttackSpeed  { get; set; } = 1.0f;
    public sealed override    float Speed        { get; set; } = 1.5f;
    public sealed override    int   Score        { get; set; } = 20;

    public sealed override Dictionary<string, float> Modifiers => new()
    {
        { "Health", 1.5f },
        { "AttackDamage", 2.0f },
        { "AttackSpeed", 1.0f },
        { "Speed", 0.75f },
        { "Score", 2.0f }
    };
}

public class DaggerEnemy : EnemyTypes
{
    public sealed override float Health { get; set; } = 5;

    public sealed override EnemyController.EnemyType Type { get; set; } = EnemyController.EnemyType.DAGGER;

    public sealed override int   AttackDamage { get; set; } = 1;
    public sealed override    float AttackSpeed  { get; set; } = 1.0f;
    public sealed override    float Speed        { get; set; } = 4.0f;
    public sealed override    int   Score        { get; set; } = 5;

    public override Dictionary<string, float> Modifiers => new()
    {
        { "Health", 0.5f },
        { "AttackDamage", 0.2f },
        { "AttackSpeed", 1.0f },
        { "Speed", 2.0f },
        { "Score", 0.5f }
    };

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