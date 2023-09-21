
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;
[ExecuteInEditMode()]
public class SplineEditor : MonoBehaviour
{
    [SerializeField]
    private SplineContainer m_splineContainer;

    [SerializeField]
    private int m_splineIndex;
    [SerializeField]
    [Range(0f, 1f)]
    private float m_time;

    float3 position;
    float3 forward;
    float3 upVector;
    [SerializeField]
    private float m_width;
    private Vector3 p1,p2;

    private void Update()
    {
       
    }
    public void SplineEditorWidth( float t, out Vector3 p1, out Vector3 p2)
    {
        m_splineContainer.Evaluate(m_splineIndex, m_time, out position, out forward, out upVector);
        //Tangent is the (forward) direction of travel along the spline to the next point;
        //Find the *right* direction based on this
        float3 right = Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right * m_width);
        p2 = position +(-right * m_width);
    }
    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.SphereHandleCap(0, p1, Quaternion.identity, 1f, EventType.Repaint);
        Handles.SphereHandleCap(1, p2, Quaternion.identity, 1f, EventType.Repaint);
    }
}