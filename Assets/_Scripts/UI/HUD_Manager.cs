using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private Transform m_carNeedleRPM, m_carNeedleKMH;
    [SerializeField] private TextMeshProUGUI speedText;
    private CarController carController;
    // Rotate  -212 / MaxSpeed  = x / CurrentSpeed

    private void Start()
    {
        carController = CarController.Instance;
    }
    // Start is called before the first frame update
    private void Update_HUD()
    {
       
        m_carNeedleKMH.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * (carController.HUD_current_KMH / carController.HUD_MaxSpeed)));

        m_carNeedleRPM.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * carController.HUD_current_RPM / carController.maxMotorTorque));
        speedText.text = ((int)carController.HUD_current_KMH).ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Update_HUD();
    }
}
