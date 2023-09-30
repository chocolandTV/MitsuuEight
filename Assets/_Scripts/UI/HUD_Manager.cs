using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private Transform m_carNeedleRPM, m_carNeedleKMH;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private List<Color> LifeContainerColor;
    [SerializeField] private GameObject lifeContainerFillObject;
    [SerializeField]private List<Sprite> number;
    private CarUserControl m_carUserControl;
    private UnityEngine.UI.Image lifeContainer;
    // Rotate  -212 / MaxSpeed  = x / CurrentSpeed

    private void Start()
    {
        m_carUserControl = CarUserControl.Instance;
        lifeContainer = lifeContainerFillObject.GetComponent<UnityEngine.UI.Image>();
    }
    // Start is called before the first frame update
    private void Update_HUD()
    {

        m_carNeedleKMH.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * (m_carUserControl.Current_KMH / m_carUserControl.MaxSpeed)));

        m_carNeedleRPM.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * m_carUserControl.Current_Life / 100));
        
        ChangeLife(m_carUserControl.Current_Life/100);
        speedText.text = ((int)m_carUserControl.Current_KMH).ToString();
    }
    private void ChangeLife(float life)
    {
        lifeContainer.fillAmount = life;
        Color _color = LifeContainerColor[0];
        if(life < 0.85f)
            _color = LifeContainerColor[1];
        if(life < 0.65f)
            _color = LifeContainerColor[2];
        if(life < 0.5f)
            _color = LifeContainerColor[3];
        if(life < 0.3f)
            _color = LifeContainerColor[4];
        if(life < 0.1f)
            _color = LifeContainerColor[5];

        if(_color != lifeContainer.color)
        {
            lifeContainer.color = _color;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Update_HUD();
    }
}
