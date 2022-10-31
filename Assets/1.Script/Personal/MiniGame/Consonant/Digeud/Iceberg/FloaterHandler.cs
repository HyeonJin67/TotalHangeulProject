using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����η� ��Ʈ��
public class FloaterHandler : MonoBehaviour
{
    #region ����
    SeaManager seaManager;

    public Rigidbody rb;
    
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;    
    // �����η� ����
    public int floaterCount;
    // ���� ����
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;

    public float accel = 1;
    //public float waterHeight = 0f;
    #endregion

    #region �Լ�

    private void Awake()
    {   
        seaManager = FindObjectOfType<SeaManager>();
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = seaManager.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight * accel) {
            float displacementMultiplier = Mathf.Clamp01((waveHeight * accel - transform.position.y) / depthBeforeSubmerged ) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f) ,transform.position ,ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
    #endregion
}