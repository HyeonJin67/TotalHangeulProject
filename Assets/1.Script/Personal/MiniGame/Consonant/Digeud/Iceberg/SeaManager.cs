using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �޽� �ⷷ�Ÿ�
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SeaManager : MonoBehaviour
{
    #region ����
    MeshFilter meshFilter;

    [Header("�ĵ� ������")]
    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float offset = 0f;

    #endregion

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = GetWaveHeight(transform.position.x + vertices[i].x);
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateNormals();

        offset += Time.deltaTime * speed;
    }


    public float GetWaveHeight(float _x)
    {
        return amplitude * Mathf.Sin(_x / length + offset);
    }

}