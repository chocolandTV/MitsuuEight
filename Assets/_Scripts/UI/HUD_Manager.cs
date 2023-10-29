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
    [SerializeField] private GameObject ShowTimeTextValue, ShowTimeTextDefault, CarCollectedIcon;
    [SerializeField] private RawImage CollectableIcon;
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
        changeSprites[0].enabled = true;
        changeSprites[0].texture = numbers[1];
        changeSprites[1].texture = numbers[0];
        changeSprites[2].texture = numbers[0];
        CarCollectedIcon.SetActive(false);
    }
    // Start is called before the first frame update
    private void Update_HUD()
    {

        m_carNeedleKMH.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * (m_carUserControl.Current_KMH / m_carUserControl.MaxSpeed)));
        m_carNeedleRPM.transform.rotation = Quaternion.Euler(0, 0, 19 - Mathf.Abs(229 * m_carUserControl.Current_Nitro / 100));
        speedText.text = ((int)m_carUserControl.Current_KMH * 6).ToString();
        time_text.text = TimeSpan.FromSeconds(Time.timeSinceLevelLoad - StageStartTime).ToString("m\\:ss\\.fff");

    }
    public void SetTimerText(bool value)
    {
        ShowTimeTextValue.SetActive(value);
        ShowTimeTextDefault.SetActive(!value);
    }

    public void UpdateTimeLapRoundText(int RoundID, float time)
    {
        RoundTextList[RoundID].SetActive(true);
        RoundTextList[RoundID].GetComponent<TextMeshProUGUI>().text = TimeSpan.FromSeconds(time).ToString("m\\:ss\\.fff");
    }
    public void ResetTimeLapRoundsText()
    {
        for (int i = 0; i < RoundTextList.Count; i++)
        {
            RoundTextList[i].SetActive(false);
        }
    }
    public void UpdateLapRound(int lapCurrent)
    {
        // LAP ROUND
        changeSprites[3].texture = numbers[lapCurrent];
    }
    public void UpdateCollectables(int value)
    {
        changeSprites[5].texture = numbers[value];
    }
    public void ChangeLife(float life)
    {
        if (life == 100)
        {
            changeSprites[0].enabled = true;
            changeSprites[0].texture = numbers[1];
            changeSprites[1].texture = numbers[0];
            changeSprites[2].texture = numbers[0];
        }
        else
        {
            changeSprites[0].enabled = false;
            int _y = (int)life / 10;
            changeSprites[1].texture = numbers[_y];
            int _z = (int)life - (_y * 10);
            changeSprites[2].texture = numbers[_z];
        }
    }
    public void ChangeNitro(float nitro)
    {
        lifeContainer.fillAmount = nitro;
        Color _color = LifeContainerColor[0];
        if (nitro < 0.85f)
            _color = LifeContainerColor[1];
        if (nitro < 0.65f)
            _color = LifeContainerColor[2];
        if (nitro < 0.5f)
            _color = LifeContainerColor[3];
        if (nitro < 0.3f)
            _color = LifeContainerColor[4];
        if (nitro < 0.1f)
            _color = LifeContainerColor[5];

        if (_color != lifeContainer.color)
        {
            lifeContainer.color = _color;
        }
    }
    public void UpdateCollectableIcon(Color color)
    {
        CollectableIcon.color = color;
    }
    public void UpdateCarCollectedIcon(bool value)
    {
        CarCollectedIcon.SetActive(value);
    }
    // Update is called once per frame
    void Update()
    {
        Update_HUD();
    }
}
