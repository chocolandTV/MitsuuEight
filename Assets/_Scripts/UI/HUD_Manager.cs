using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Manager : MonoBehaviour
{
    [SerializeField] private Transform m_carNeedleRPM, m_carNeedleKMH;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private List<Color> LifeContainerColor;
    [SerializeField] private GameObject lifeContainerFillObject;
    [SerializeField] private List<RawImage> changeSprites;
    [SerializeField] private List<GameObject> RoundTextList;
    [SerializeField] private TextMeshProUGUI time_text;
    [SerializeField] private List<Texture2D> numbers;
    private CarUserControl m_carUserControl;
    public static float StageStartTime;
    public static HUD_Manager Instance { get; private set; }
    private UnityEngine.UI.Image lifeContainer;
    // Rotate  -212 / MaxSpeed  = x / CurrentSpeed
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    private void Start()
    {
        m_carUserControl = CarUserControl.Instance;
        lifeContainer = lifeContainerFillObject.GetComponent<UnityEngine.UI.Image>();
        changeSprites[4].texture = numbers[LapManager.Instance.GetLapRounds()];
        changeSprites[3].texture = numbers[CollectionManager.CoinWallet];
    }
    // Start is called before the first frame update
    private void Update_HUD()
    {

        m_carNeedleKMH.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * (m_carUserControl.Current_KMH / m_carUserControl.MaxSpeed)));

        m_carNeedleRPM.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * m_carUserControl.Current_Nitro / 100));

        ChangeLife(m_carUserControl.Current_Nitro / 100);
        speedText.text = ((int)m_carUserControl.Current_KMH*6).ToString();
        if (m_carUserControl.Current_Life == 100)
        {
            changeSprites[0].enabled = true;
            changeSprites[0].texture = numbers[1];
            changeSprites[1].texture = numbers[0];
            changeSprites[2].texture = numbers[0];
        }
        else
        {
            changeSprites[0].enabled = false;
            int _y = (int)m_carUserControl.Current_Life / 10;
            changeSprites[1].texture = numbers[_y];
            int _z = (int)m_carUserControl.Current_Life - (_y * 10);
            changeSprites[2].texture = numbers[_z];
        }
        
        time_text.text = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - StageStartTime).ToString("m\\:ss\\.fff");

    }
    public void UpdateTimeLapRoundText(int RoundID, float time)
    {
        RoundTextList[RoundID].SetActive(true);
        RoundTextList[RoundID].GetComponent<TextMeshProUGUI>().text = TimeSpan.FromSeconds(time).ToString("m\\:ss\\.fff");
    }
    public void UpdateLapRound()
    {
        // LAP ROUND
        changeSprites[3].texture = numbers[LapManager.Lap_Current];
    }
    public void UpdateCollectables()
    {
        changeSprites[3].texture = numbers[CollectionManager.CoinWallet];
    }
    private void ChangeLife(float life)
    {
        lifeContainer.fillAmount = life;
        Color _color = LifeContainerColor[0];
        if (life < 0.85f)
            _color = LifeContainerColor[1];
        if (life < 0.65f)
            _color = LifeContainerColor[2];
        if (life < 0.5f)
            _color = LifeContainerColor[3];
        if (life < 0.3f)
            _color = LifeContainerColor[4];
        if (life < 0.1f)
            _color = LifeContainerColor[5];

        if (_color != lifeContainer.color)
        {
            lifeContainer.color = _color;
        }
    }
    // Update is called once per frame
    void Update()
    {
        Update_HUD();
    }
}
