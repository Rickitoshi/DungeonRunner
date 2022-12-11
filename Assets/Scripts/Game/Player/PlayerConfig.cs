using Unity.VisualScripting;
using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Game/PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Move")]
        [SerializeField] private float moveSpeed ;
        [SerializeField] private float strafeDuration;
        [SerializeField] private float strafeDistance;
        [SerializeField] private float jumpHeight;
        [Space(10f),Header("Stats")]
        [SerializeField, Min(1)] private int maxHealth ;
        [SerializeField] private float magnetDuration;

        public float MoveSpeed => moveSpeed;

        public float StrafeDuration => strafeDuration;

        public float StrafeDistance => strafeDistance;

        public float JumpHeight => jumpHeight;

        public int MaxHealth => maxHealth;

        public float MagnetDuration => magnetDuration;
    }
}