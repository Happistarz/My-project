using UnityEngine;

public class Constants : MonoBehaviour
{
    public const string ENEMY_TAG  = "Enemy";
    
    public const float HEALTHBAR_MAX_DISTANCE = 35.0f;
    
    public static readonly Color CRITICAL_COLOR      = Color.red;
    public static readonly Color ATTACKSPEED_COLOR   = Color.yellow;
    public static readonly Color MOVEMENTSPEED_COLOR = Color.green;
    public static readonly Color BEACON_COLOR        = Color.blue;

    public const float CRITICAL_MULTIPLIER      = 2.0f;
    
    public const float ENEMY_SPAWN_RATE          = 2.0f;
    public const int   ENEMY_WAVE_SIZE           = 10;

    public const string CRITICAL_ICON       = "critical";
    public const string ATTACK_SPEED_ICON   = "attack_speed";
    public const string MOVEMENT_SPEED_ICON = "move_speed";
    public const string BEACON_ICON         = "beacon";
}