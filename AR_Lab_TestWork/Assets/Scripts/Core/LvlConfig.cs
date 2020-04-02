using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New LvL Config", menuName = "Configs/LvLConfig")]
public class LvlConfig : ScriptableObject
{
    [Header("Planes params")]
    public int PlaneHP;
    public ValueRange PlaneDamagePreset1;
    public ValueRange PlaneDamagePreset2;
    public ValueRange PlaneDamagePreset3;
    public ValueRange PlaneStartPositionX;
    public ValueRange PlaneStartPositionY;
    public float PlaneStartPostionZ;
    [Space]

    [Header("Player params")]
    public int PlayerHP;
    public ValueRange PlayerDamage;
    public ValueRange HorizontalBounds;
    public ValueRange VerticalBounds;
    public float PlayerSpeed;
    [Space]

    [Header("Boss params")]
    public int BossPlaneHP;
    public Vector3 BossStartPosition;

    [Header("Bullet params")]
    public float BulletVelocity;
    public float BulletLifeTime;
    
    [System.Serializable]
    public struct ValueRange
    {
        public int MinValue;
        public int MaxValue;
    }

    public Vector3 GetPlaneStartPosition()
    {
        return new Vector3(
            Random.Range(PlaneStartPositionX.MinValue, PlaneStartPositionX.MaxValue),
            Random.Range(PlaneStartPositionY.MinValue, PlaneStartPositionY.MaxValue), 
            PlaneStartPostionZ);
    }
}
