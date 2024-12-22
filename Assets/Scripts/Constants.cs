using UnityEngine;

public class Constants : MonoBehaviour
{
    public static readonly string PLAYER_TAG = "Player";
    public static readonly string ENEMY_TAG = "Enemy";
    public static readonly string BULLET_TAG = "Bullet";

    public static readonly Color CRITICAL_COLOR = Color.red;
    public static readonly Color ATTACKSPEED_COLOR = Color.yellow;
    public static readonly Color MOVEMENTSPEED_COLOR = Color.green;
    public static readonly Color BEACON_COLOR = Color.blue;
    
    public static readonly float CRITICAL_MULTIPLIER = 2.0f;
    public static readonly float ATTACKSPEED_MULTIPLIER = 2.0f;
    public static readonly float MOVEMENTSPEED_MULTIPLIER = 2.0f;
    public static readonly float BEACON_MULTIPLIER = 2.0f;
    
    public static readonly float ENEMY_SPAWN_RATE = 1.0f;
    public static readonly float ENEMY_SPAWN_RATE_DECREASE = 0.1f;
}
