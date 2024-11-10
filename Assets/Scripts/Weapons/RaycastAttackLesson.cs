using System;
using System.Collections;
using Code.Source.Lessons.Base;
using Code.Source.Lessons.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Source.Lessons.RaycastAttack
{
    public class RaycastAttackLesson : AttackBehaviour
    {
        public short idGun;
        [Header("Damage")]
        [SerializeField, Min(0f)] private int _damage = 25;

        [Header("Режимы стрельбы")]
        [SerializeField] public bool canShoot = true;
        [SerializeField] public bool isAuto;
        [SerializeField] public float delayShoot = 0.2f;
        [SerializeField] public float recoilTime = 0.15f;
        [SerializeField] private bool isShooting;
        [SerializeField] private bool isRecoil;
        [SerializeField] private bool canAutoReload;
        [SerializeField] private float reloadAfterTime = 0.2f;

        [Header("Патроны")]
        [SerializeField] public int ammoCurrent = 7;
        [SerializeField] public int ammoFull = 7;
        [SerializeField] public int ammoPack = 49;
        [SerializeField] public int ammoPackFull = 49;
        [SerializeField] public float reloadTime = 3f;
        [SerializeField] public bool isReloading;

        [Header("Отдача")]
        [SerializeField] public float xRecoil; // Отдача по x
        [SerializeField] public float yRecoil; // Отдача по y

        [Header("Ray")]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField, Min(0)] private float _distance = Mathf.Infinity;
        [SerializeField, Min(0)] private int _shotCount = 1;

        [Header("Spread")]
        [SerializeField] private bool _useSpread;
        [SerializeField, Min(0)] private float _spreadFactor = 1f;
        private float _startSpreadFactor = 1f;

        [Header("Particle System")]
        [SerializeField] private ParticleSystem _muzzleEffect;
        [SerializeField] private ParticleSystem _hitEffectPrefab;
        [SerializeField, Min(0f)] private float _hitEffectDestroyDelay = 2f;

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _audioClipEmpty;

        [Header("Debug")]
        [SerializeField] private DrawRayType _drawRayType = DrawRayType.Raycast;

        [Header("Анимация")]
        public Animator animWeapon;
        public GameObject hitMarker;

        private float x1;
        private float y1;
        private float z1;

        private void Start()
        {
            if (PlayerPrefs.HasKey("ItemsClass"))
            {
                SaveAll saveAll = GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>();
                _spreadFactor -= _spreadFactor * saveAll.saveItemsClass.skillSpread * 0.05f;

                _damage += (int)(_damage * saveAll.saveItemsClass.skillDamage * 0.05f);

                ammoCurrent += (int)(ammoCurrent * saveAll.saveItemsClass.skillAmmoPlus * 0.05f);
                ammoFull += (int)(ammoFull * saveAll.saveItemsClass.skillAmmoPlus * 0.05f);

                xRecoil -= xRecoil * saveAll.saveItemsClass.skillRecoil * 0.05f;
                yRecoil -= yRecoil * saveAll.saveItemsClass.skillRecoil * 0.05f;
            }
            _startSpreadFactor = _spreadFactor;
        }

        //[ContextMenu(nameof(PerformAttack))]
        public override void PerformAttack()
        {
            for (var i = 0; i < _shotCount; i++)
            {
                PerformRaycast(i);
            }
            ammoCurrent--;
            PerformEffects();
        }
        private void Update()
        {
            if (!GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<PauseScript>().isPause)
            {
                animWeapon.SetBool("shooting", isRecoil);

                if (Input.GetMouseButtonDown(0) && ammoCurrent > 0 && !isShooting && canShoot)
                {
                    isShooting = true;
                    if (isAuto)
                        StartCoroutine(ShootCorutine());
                    else
                        StartCoroutine(OneShootCorutine());
                }
                if (Input.GetMouseButtonUp(0) || ammoCurrent <= 0)
                {
                    if (isAuto)
                    {
                        isShooting = false;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().xRecoil = 0;
                        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().yRecoil = 0;
                    }
                }
                if (Input.GetMouseButtonDown(0) && ammoCurrent <= 0)
                {
                    if (_audioSource != null && _audioClip != null)
                    {
                        _audioSource.PlayOneShot(_audioClipEmpty);
                    }
                }
                if (Input.GetKeyDown(KeyCode.R) && !isShooting && ammoCurrent <= ammoFull)
                {
                    StartCoroutine(ReloadCorutine());
                }
                if (canAutoReload && ammoCurrent <= 0)
                {
                    StartCoroutine(ReloadCorutine());
                }
                if (ammoPack > ammoPackFull)
                    ammoPack = ammoPackFull;
                PlController plMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlController>();


                if (!plMove.isWalk && !plMove.isRun)
                {
                    if (GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().saveItemsClass.currentGun != 1)
                        _spreadFactor = _startSpreadFactor / 20;
                    if (GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().saveItemsClass.currentGun == 1)
                        _spreadFactor = _startSpreadFactor / 2;
                }
                if (plMove.isWalk && !plMove.isRun)
                    _spreadFactor = _startSpreadFactor;
                if (plMove.isWalk && plMove.isRun)
                    _spreadFactor = _startSpreadFactor * 1.5f;

            }

        }

        private void PerformRaycast(int countShot)
        {
            var direction = _useSpread ? transform.forward + CalculateSpread(countShot) : transform.forward;
            var ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _layerMask))
            {
                var hitCollider = hitInfo.collider;

                if (hitCollider.tag == "Enemy")
                {
                    Debug.Log("Попадание по врагу");
                    hitCollider.GetComponentInParent<EnemyScript>().giveDamage(_damage);
                    StartCoroutine(HitMarkerCoroutine());
                }
                if (hitCollider.tag == "Boss")
                {
                    if (hitCollider.GetComponentInParent<BossScript>() != null)
                        hitCollider.GetComponentInParent<BossScript>().giveDamage(_damage);
                    if (hitCollider.GetComponentInParent<BossScript1>() != null)
                        hitCollider.GetComponentInParent<BossScript1>().giveDamage(_damage);
                    if (hitCollider.GetComponentInParent<BossScript2>() != null)
                        hitCollider.GetComponentInParent<BossScript2>().giveDamage(_damage);
                    if (hitCollider.GetComponentInParent<BossScript3>() != null)
                        hitCollider.GetComponentInParent<BossScript3>().giveDamage(_damage);
                    if (hitCollider.GetComponentInParent<BossScript4>() != null)
                        hitCollider.GetComponentInParent<BossScript4>().giveDamage(_damage / 2);
                    StartCoroutine(HitMarkerCoroutine());
                }
                if (hitCollider.tag == "SpikeBossPart2")
                {
                    GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript4>().giveDamage(_damage);
                    StartCoroutine(HitMarkerCoroutine());
                }
                if (hitCollider.tag != "Player")
                    SpawnParticleEffectOnHit(hitInfo);
                Debug.Log("Попадание по любой поверхности - " + hitCollider.GetComponent<Transform>().position.x + ")(" +
                    hitCollider.GetComponent<Transform>().position.y + ")(" +
                    hitCollider.GetComponent<Transform>().position.z);
            }
        }

        private void PerformEffects()
        {
            if (_muzzleEffect != null)
            {
                _muzzleEffect.Play();
            }

            if (_audioSource != null && _audioClip != null)
            {
                _audioSource.PlayOneShot(_audioClip);
            }
        }

        private void SpawnParticleEffectOnHit(RaycastHit hitInfo)
        {
            if (_hitEffectPrefab != null)
            {
                var hitEffectRotation = Quaternion.LookRotation(hitInfo.normal);
                var hitEffect = Instantiate(_hitEffectPrefab, hitInfo.point, hitEffectRotation);

                Destroy(hitEffect.gameObject, _hitEffectDestroyDelay);
            }
        }

        private Vector3 CalculateSpread(int countShot)
        {
            if (GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().saveItemsClass.currentGun == 1)
            {
                if (countShot < 4)
                {
                    x1 = Random.Range(-_spreadFactor / 5, _spreadFactor / 5);
                    y1 = Random.Range(-_spreadFactor / 5, _spreadFactor / 5);
                    z1 = Random.Range(-_spreadFactor / 5, _spreadFactor / 5);
                }
                else
                {
                    x1 = Random.Range(-_spreadFactor, _spreadFactor);
                    y1 = Random.Range(-_spreadFactor, _spreadFactor);
                    z1 = Random.Range(-_spreadFactor, _spreadFactor);
                }
            }
            if (GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<SaveAll>().saveItemsClass.currentGun != 1)
            {
                x1 = Random.Range(-_spreadFactor, _spreadFactor);
                y1 = Random.Range(-_spreadFactor, _spreadFactor);
                z1 = Random.Range(-_spreadFactor, _spreadFactor);
            }
            return new Vector3
            {
                x = x1,
                y = y1,
                z = z1
            };
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_drawRayType == DrawRayType.None)
                return;

            var ray = new Ray(transform.position, transform.forward);

            switch (_drawRayType)
            {
                case DrawRayType.None: break;
                case DrawRayType.Raycast: DrawRaycast(ray); break;
                case DrawRayType.BoxCast: DrawBoxCast(ray); break;
                case DrawRayType.SphereCast: DrawSphereCast(ray); break;

                default: throw new ArgumentOutOfRangeException(nameof(_drawRayType));
            }
        }

        private void DrawRaycast(Ray ray)
        {
            if (Physics.Raycast(ray, out var hitInfo, _distance, _layerMask))
            {
                DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
            }
            else
            {
                var hitPosition = ray.origin + ray.direction * _distance;

                DrawRay(ray, hitPosition, _distance, Color.green);
            }
        }

        private void DrawBoxCast(Ray ray)
        {
            var boxHalfExtents = transform.lossyScale / 2f;

            if (Physics.BoxCast(ray.origin, boxHalfExtents, ray.direction,
                    out var hitInfo, transform.rotation, _distance, _layerMask))
            {
                DrawRay(ray, ray.origin + ray.direction * hitInfo.distance, hitInfo.distance, Color.red);
            }
            else
            {
                DrawRay(ray, ray.origin + ray.direction * _distance, _distance, Color.green);
            }
        }

        private void DrawSphereCast(Ray ray)
        {
            var sphereRadius = transform.lossyScale.x / 2f;

            if (Physics.SphereCast(ray.origin, sphereRadius, ray.direction, out var hitInfo, _distance, _layerMask))
            {
                DrawRay(ray, ray.origin + ray.direction * hitInfo.distance, hitInfo.distance, Color.red);
            }
            else
            {
                DrawRay(ray, ray.origin + ray.direction * _distance, _distance, Color.green);
            }
        }

        private void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
        {
            const float hitPointRadius = 0.15f;

            Debug.DrawRay(ray.origin, ray.direction * distance, color);

            Gizmos.color = color;

            switch (_drawRayType)
            {
                case DrawRayType.None: break;
                case DrawRayType.Raycast: Gizmos.DrawSphere(hitPosition, hitPointRadius); break;
                case DrawRayType.BoxCast: Gizmos.DrawWireCube(hitPosition, transform.lossyScale); break;
                case DrawRayType.SphereCast: Gizmos.DrawWireSphere(hitPosition, transform.lossyScale.x / 2f); break;

                default: throw new ArgumentOutOfRangeException(nameof(_drawRayType), _drawRayType, null);
            }
        }
#endif

        private enum DrawRayType
        {
            None,
            Raycast,
            BoxCast,
            SphereCast
        }



        IEnumerator ShootCorutine()
        {
            while (canShoot && isShooting && ammoCurrent > 0)
            {
                PerformAttack();
                StartCoroutine(RecoilCorutine());
                yield return new WaitForSeconds(delayShoot);
            }
        }

        IEnumerator OneShootCorutine()
        {
            PerformAttack();
            StartCoroutine(RecoilCorutine());
            yield return new WaitForSeconds(delayShoot);
            isShooting = false;
        }

        IEnumerator RecoilCorutine()
        {
            isRecoil = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().xRecoil = xRecoil;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().yRecoil = yRecoil;
            yield return new WaitForSeconds(recoilTime);
            isRecoil = false;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().xRecoil = 0;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>().yRecoil = 0;
        }
        public IEnumerator ReloadCorutine()
        {
            if (ammoCurrent < ammoFull && ammoPack > 0)
            {
                canShoot = false;
                animWeapon.SetBool("reloading", true);
                yield return new WaitForSeconds(reloadTime);
                animWeapon.SetBool("reloading", false);
                if (ammoCurrent == 0 && ammoPack >= ammoFull) // Если обойма пуста
                {
                    ammoCurrent = ammoFull;
                    //ammoPack -= ammoCurrent; // Вычетаем из количества патронов в кармане текущее количество патронов
                }
                if (ammoCurrent == 0 && ammoPack > 0) // Если обойма пуста
                {
                    ammoCurrent += ammoPack;
                    //ammoPack -= ammoPack; // Вычетаем из количества патронов в кармане количество патронов в кармане
                }
                if (ammoCurrent > 0 && ammoPack < ammoFull - ammoCurrent) // Если обойма пуста
                {
                    ammoCurrent += ammoPack;
                    //ammoPack -= ammoPack; // Вычетаем из количества патронов в кармане количество патронов в кармане
                }
                if (ammoCurrent > 0 && ammoPack >= ammoFull - ammoCurrent) // Если обойма не пуста
                {
                    int needAmmo;
                    needAmmo = ammoFull - ammoCurrent;
                    ammoCurrent += needAmmo;
                    //ammoPack -= needAmmo; // Вычетаем из количества патронов в кармане количество требуемых патронов для дозарядки полной обоймы
                }
                yield return new WaitForSeconds(reloadAfterTime);
                canShoot = true;
            }

        }

        IEnumerator HitMarkerCoroutine()
        {
            hitMarker.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            hitMarker.SetActive(false);
        }
    }
}