
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
    float3 tangent;
    float3 upVector;

    private void Update()
    {
       m_splineContainer.Evaluate(m_splineIndex, m_time, out position, out tangent, out upVector);
    
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Vector3.zero, Vector3.one *5);
        Gizmos.DrawCube(position, Vector3.one*5);
       
        
    }
}