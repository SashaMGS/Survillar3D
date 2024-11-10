using UnityEngine;

namespace NewShootingSystem
{
    public class PoolObjects : MonoBehaviour
    {
        public int poolCount; //  ���������� ������

        public Transform poolParent; // ��������, � ������� ��������� �����

        public GameObject prefab; // ������ ��������, ������� ����� �����������

        private int currentPoolElementId = 0; // ������� ���� �������, ������� �������� ����� ������ ������������ ���������

        private GameObject[] poolAR; // ������ ������ ��� ������ � ������� � �������



        private void Awake()
        {
            poolParent = transform; // ������������� �������� �������
            poolAR = new GameObject[poolCount]; // �������������� ������
            for (int i = 0; i < poolCount; i++) // ��������� ������ �������
            {
                poolAR[i] = Instantiate(prefab, transform.position, transform.rotation, poolParent);
                poolAR[i].SetActive(false);
            }
        }

        public void PlusClon(Transform spawnPlace)
        {
            GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // ������ ������ �� �������� �������


            //-----------------------����������� �������������� ����� � ����������� �� ������� ���������---------------------------

            obj.transform.position = spawnPlace.position; // ������������� ������� �����

            obj.SetActive(true); // �������� �����
            if (GetComponent<ParticleSystem>())
                GetComponent<ParticleSystem>().Play();
            if (obj.transform.GetChild(0).GetComponent<AddForceRigidBody>())
                obj.transform.GetChild(0).GetComponent<AddForceRigidBody>().Fly();

            obj.transform.rotation = spawnPlace.transform.rotation; // ������������� ������� �����
            currentPoolElementId++; // �������� ��������� ������ ��� �����������
            if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // ���� ������ ����� �� ����� ����, �������� � ������
        }

        public void PlusClon(Vector3 spawnPlace, Quaternion spawnRotate)
        {
            GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // ������ ������ �� �������� �������


            //-----------------------����������� �������������� ����� � ����������� �� ������� ���������---------------------------

            obj.transform.position = spawnPlace; // ������������� ������� �����

            obj.SetActive(true); // �������� �����
            if (obj.GetComponent<ParticleSystem>())
                obj.GetComponent<ParticleSystem>().Play();
            if (obj.GetComponent<AddForceRigidBody>())
                obj.GetComponent<AddForceRigidBody>().Fly();

            obj.transform.rotation = spawnRotate; // ������������� ������� �����
            currentPoolElementId++; // �������� ��������� ������ ��� �����������
            if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // ���� ������ ����� �� ����� ����, �������� � ������
        }

        [System.Obsolete]
        public void PlusClon(Vector3 spawnPlace, Quaternion spawnRotate, Color color)
        {
            GameObject obj = poolParent.GetChild(currentPoolElementId).gameObject; // ������ ������ �� �������� �������


            //-----------------------����������� �������������� ����� � ����������� �� ������� ���������---------------------------

            obj.transform.position = spawnPlace; // ������������� ������� �����

            obj.SetActive(true); // �������� �����
            if (obj.GetComponent<ParticleSystem>())
            {
                obj.GetComponent<ParticleSystem>().Play();
                obj.GetComponent<ParticleSystem>().startColor = color;
            }
            if (obj.GetComponent<AddForceRigidBody>())
                obj.GetComponent<AddForceRigidBody>().Fly();

            obj.transform.rotation = spawnRotate; // ������������� ������� �����
            currentPoolElementId++; // �������� ��������� ������ ��� �����������
            if (currentPoolElementId > poolParent.childCount - 1) currentPoolElementId = 0; // ���� ������ ����� �� ����� ����, �������� � ������
        }
    }
}
