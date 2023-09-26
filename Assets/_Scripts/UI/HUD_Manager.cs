using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private Transform m_carNeedleRPM, m_carNeedleKMH;
    [SerializeField] private TextMeshProUGUI speedText;
    private CarUserControl m_carUserControl;
    // Rotate  -212 / MaxSpeed  = x / CurrentSpeed

    private void Start()
    {
        m_carUserControl = CarUserControl.Instance;
    }
    // Start is called before the first frame update
    private void Update_HUD()
    {
       
        // m_carNeedleKMH.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * (m_carUserControl.Current_KMH / m_carUserControl.MaxSpeed)));

        // m_carNeedleRPM.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * m_carUserControl.Current_Boost));
        // speedText.text = ((int)m_carUserControl.Current_KMH).ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // Update_HUD();
    }
}
