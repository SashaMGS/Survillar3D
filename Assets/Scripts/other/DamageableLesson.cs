using Code.Source.Lessons.Interfaces;
using UnityEngine;

namespace Code.Source.Lessons.Base
{
    public class DamageableLesson : MonoBehaviour, IDamageable
    {
        public void ApplyDamage(float damage)
        {
            Debug.Log($"Damage {damage} is applied!");
        }
    }
}