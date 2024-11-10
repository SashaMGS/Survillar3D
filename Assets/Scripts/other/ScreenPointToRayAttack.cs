using Code.Source.Lessons.Interfaces;
using UnityEngine;

namespace Code.Source.Lessons.RaycastAttack
{
    public class ScreenPointToRayAttack : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Min(0f)] private float _damage = 10f;
        
        private const int LeftMouseButtonIndex = 0;
        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(LeftMouseButtonIndex))
            {
                PerformAttack();
            }
        }

        private void PerformAttack()
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            {
                if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(_damage);
                }
            }
        }
    }
}