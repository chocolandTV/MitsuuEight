
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.Splines;
[ExecuteInEditMode()]
public class SplineEditor : MonoBehaviour
{
    [SerializeField]
    private SplineContainer m_splineContainer;

    [SerializeField]private int m_splineIndex;
    [SerializeField][Range(0f, 1f)]private float m_time;

    float3 position;
    float3 forward;
    float3 upVector;
    [SerializeField]private float m_width = 20f;
    private Vector3 p1,p2;
    public void SampleSpineWidth(float _time, out Vector3 p1, out Vector3 p2)
    {
        m_splineContainer.Evaluate(m_splineIndex, _time, out position, out forward, out upVector);
        
        float3 right = Vector3.Cross(forward, upVector).normalized;
        p1 = position + (right*m_width);
        p2 = position + (-right*m_width);
    }
    
    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawCube(p1, Vector3.one *10);
    //     Gizmos.DrawCube(p2, Vector3.one*10);
    //     Gizmos.DrawLine(p1,p2);
       
        
    // }
}