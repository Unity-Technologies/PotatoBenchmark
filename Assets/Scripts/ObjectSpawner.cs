// Code adapted from Arnaud's SRP Batcher
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectSpawner : MonoBehaviour
{
    public Material[] m_MaterialList;
    public Transform m_SpawnOrigin;
    public GameObject m_LongTriangleObject;
    public int m_SpawnSize = 32;
    public int m_LongTriangleSize = 50;
    public string m_MaterialColorPropName = "_BaseColor";

    GameObject[] m_GameObjects;
    Vector3[] m_Positions;

    float m_Clock;

    void Start()
    {
        m_GameObjects = new GameObject[m_SpawnSize * m_SpawnSize];
        m_Positions = new Vector3[m_SpawnSize * m_SpawnSize];

        float offset = (m_SpawnSize / 2); 
        Vector3 spawnOrigin = m_SpawnOrigin.position;
        Vector3 spawnPos = new Vector3(spawnOrigin.x - offset, spawnOrigin.y, spawnOrigin.z - offset);

        Random.InitState((int)Time.time);
        PrimitiveType[] primitiveTypes = {PrimitiveType.Capsule, PrimitiveType.Cube, PrimitiveType.Sphere, PrimitiveType.Cylinder};

        int index = 0;
        for (int i = 0; i < m_SpawnSize; ++i)
        {
            for (int j = 0; j < m_SpawnSize; ++j)
            {
                int primitiveIndex = Random.Range(0, primitiveTypes.Length);
                Vector3 spawnPosition = new Vector3(spawnPos.x + i, spawnPos.y, spawnPos.z + j);

                GameObject instance = GameObject.CreatePrimitive(primitiveTypes[primitiveIndex]);
                SetupInstance(instance);
                m_GameObjects[index] = instance;
                m_Positions[index] = spawnPosition;
                index++;
            }
        }

        spawnPos = new Vector3(spawnOrigin.x, spawnOrigin.y - (m_LongTriangleSize / 2), spawnOrigin.z);
        for (int i = 0; i < m_LongTriangleSize; ++i)
        {
            Vector3 pos = new Vector3(spawnPos.x, spawnPos.y + i * 0.5f, spawnPos.z);
            GameObject instance = GameObject.Instantiate(m_LongTriangleObject, pos, Quaternion.Euler(0.0f, (360.0f * i)/ m_LongTriangleSize, 0.0f));
            SetupInstance(instance);
        }
    }

    void SetupInstance(GameObject instance)
    {
        instance.transform.parent = m_SpawnOrigin;
        DestroyImmediate(instance.GetComponent<Collider>());
        MeshRenderer renderer = instance.GetComponent<MeshRenderer>();
        //renderer.lightProbeUsage = LightProbeUsage.Off;
        //renderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
        renderer.material = new Material(m_MaterialList[Random.Range(0, m_MaterialList.Length)]);
        renderer.material.SetColor(m_MaterialColorPropName, Random.ColorHSV(0.3f, 1f, 0.3f, 1f, 0.3f, 1f));
    }

    void Update()
    {
        m_Clock += Time.fixedDeltaTime;
        int index = 0;
        float t0 = m_Clock * 2.0f;

        for (int i = 0; i < m_SpawnSize; ++i)
        {
            float t1 = t0;
            for (int j = 0; j < m_SpawnSize; ++j)
            {
                Vector3 pos = m_Positions[index] + new Vector3(0.0f, Mathf.Sin(t1), 0.0f);
                m_GameObjects[index].transform.position = pos;
                t1 += 0.373f;
                index++;
            }

            t0 += 0.25f;
        }

        m_SpawnOrigin.transform.Rotate(Vector3.up * Time.fixedDeltaTime);
    }
}
