using UnityEngine;
using System.Collections;

namespace NewShootingSystem
{
    public class GunScript : MonoBehaviour
    {
        [Header("Ammo variables")]
        [Space(10)]
        public int _maxAmmo = 30;
        public int _curAmmo = 30;
        public float _delayReloading = 1.5f;
        [Header("Automatic variables")]
        [Space(10)]
        public bool _isAuto = false;
        public float _delayShooting = 0.15f;
        [Header("Normal variables")]
        [Space(10)]
        public float _fireRange = 100f;
        public int _damage = 10;
        [Header("Other variables")]
        [Space(10)]
        public GameObject _target;
        public Animator _anim;
        public PoolObjects _hitParticle;
        public ParticleSystem _fireParticle;
        public LayerMask _layerMask;
        [Header("Bullet down variables")]
        [Space(10)]
        public float _forceBulletDown = 99f;
        public GameObject _placeSpawnBulletDown;
        public PoolObjects _BulletDownPool;
        [Header("Recoil")]
        public bool _randomRecoilX;
        public bool _randomRecoilY;
        public float _recoilForceX = 0.1f;
        public float _recoilForceY = 0.1f;
        [Header("Deguging")]
        [Space(10)]
        public bool _debuging = true;
        [Header("Sounds")]
        [Space(10)]
        public AudioSource _audioSource;
        public AudioClip _shootAudio;
        public AudioClip _reloadAudio;
        public AudioClip _emptyAudio;

        public bool _isReloading;
        private bool _isZooming;
        private bool _canShooting = true;
        private bool _isAutoShooting;

        private void Start()
        {
            _canShooting = true;
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!GameObject.FindGameObjectWithTag("ScriptsObj").GetComponent<PauseScript>().isPause)
            {
                if (Input.GetKeyDown(KeyCode.R))
                    StartCoroutine(Reload());
                if (Input.GetMouseButtonDown(0) && _canShooting && _curAmmo > 0)
                {
                    StartCoroutine(OneShootDelay());
                    if (_isAuto && !_isAutoShooting)
                    {
                        _isAutoShooting = true;
                        StartCoroutine(ShootEn());
                    }
                    else
                        OneShoot();
                }
                else if (Input.GetMouseButtonDown(0) && _canShooting && _curAmmo <= 0)
                {
                    _audioSource.clip = _emptyAudio;
                    _audioSource.Play();
                }
            }
            if (Input.GetMouseButtonDown(1) && !_isReloading)
            {
                if (!_isZooming)
                {
                    _isZooming = true;
                    _anim.SetBool("IsZooming", _isZooming);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                if (_isZooming)
                {
                    _isZooming = false;
                    _anim.SetBool("IsZooming", _isZooming);
                }
            }
            if (Input.GetMouseButtonUp(0) || _curAmmo <= 0)
            {
                _isAutoShooting = false;
            }
        }

        private IEnumerator Reload()
        {
            _audioSource.clip = _reloadAudio;
            _audioSource.Play();
            _canShooting = false;
            _anim.SetBool("IsZooming", false);
            _anim.SetBool("IsReloading", true);
            yield return new WaitForSeconds(_delayReloading);
            _anim.SetBool("IsReloading", false);
            _curAmmo = _maxAmmo;
            _canShooting = true;
            yield break;
        }

        private IEnumerator OneShootDelay()
        {
            _canShooting = false;
            yield return new WaitForSeconds(_delayShooting);
            _canShooting = true;
            yield break;
        }

        private IEnumerator ShootEn()
        {
            while (_isAutoShooting)
            {
                if (!_isAutoShooting)
                {
                    RecoilChangeNull();
                    yield break;
                }
                OneShoot();
                RecoilChange();
                yield return new WaitForSeconds(_delayShooting / 2.5f);
                if (!_isAutoShooting)
                {
                    RecoilChangeNull();
                    yield break;
                }
                RecoilChangeNull();
                yield return new WaitForSeconds(_delayShooting / 1.5f);
                if (!_isAutoShooting)
                {
                    RecoilChangeNull();
                    yield break;
                }
            }
            RecoilChangeNull();
            yield break;
        }

        private void RecoilChange()
        {
            if (_randomRecoilX)
                Camera.main.GetComponent<CamController>().xRecoil = Random.Range(-_recoilForceX, _recoilForceX);
            else
                Camera.main.GetComponent<CamController>().xRecoil = _recoilForceX;
            if (_randomRecoilY)
                Camera.main.GetComponent<CamController>().yRecoil = Random.Range(-_recoilForceY, _recoilForceY);
            else
                Camera.main.GetComponent<CamController>().yRecoil = _recoilForceY;
        }

        private void RecoilChangeNull()
        {
            Camera.main.GetComponent<CamController>().xRecoil = 0f;
            Camera.main.GetComponent<CamController>().yRecoil = 0f;
        }

        private void OneShoot()
        {
            _BulletDownPool.PlusClon(_placeSpawnBulletDown.transform);
            _fireParticle.Play();
            _audioSource.clip = _shootAudio;
            _audioSource.Play();

            _curAmmo--;

            if (GameObject.FindGameObjectWithTag("WeaponRecoil"))
                GameObject.FindGameObjectWithTag("WeaponRecoil").GetComponent<Animator>().Play("SimpleRecoil");
            else
                Debug.Log("Object with tag 'WeaponRecoil' not found");

            RaycastHit hit;
            if (Physics.Raycast(_target.transform.position, _target.transform.forward, out hit, _fireRange, _layerMask))
            {
                if (hit.transform.GetComponentInParent<EnemyScript>())
                {
                    hit.transform.GetComponentInParent<EnemyScript>().giveDamage(_damage);
                    if (_debuging)
                        Debug.Log("Hp " + hit.transform.name + " = " + hit.transform.GetComponentInParent<EnemyScript>().hp);
                }
                else if (hit.transform.GetComponentInParent<BossScript>())
                {
                    hit.transform.GetComponentInParent<BossScript>().giveDamage(_damage);
                }
                else if (hit.transform.GetComponentInParent<BossScript1>())
                {
                    hit.transform.GetComponentInParent<BossScript1>().giveDamage(_damage);
                }
                else if (hit.transform.GetComponentInParent<BossScript2>())
                {
                    hit.transform.GetComponentInParent<BossScript2>().giveDamage(_damage);
                }
                else if (hit.transform.GetComponentInParent<BossScript3>())
                {
                    hit.transform.GetComponentInParent<BossScript3>().giveDamage(_damage);
                }
                else if (hit.transform.GetComponentInParent<BossScript4>())
                {
                    hit.transform.GetComponentInParent<BossScript4>().giveDamage(_damage);
                }
                else
                {
                    if (_debuging)
                        Debug.Log(hit.transform.name + " not have component EnemyScript or bosses script");
                }

                if (hit.transform.tag == "Enemy")
                    _hitParticle.PlusClon(hit.point, hit.transform.rotation, Color.red);
                else
                    _hitParticle.PlusClon(hit.point, hit.transform.rotation, Color.black);
            }
        }
    }
}
