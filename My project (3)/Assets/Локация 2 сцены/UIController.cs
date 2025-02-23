using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text scoreLabel;
    [SerializeField] private SettingsPopup settingsPopup;

    private int _score;

    void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); //���������, ����� ����� �������� �� ������� ENEMY_HIT.
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); //��� ���������� ������� �������� ����������, ����� �������� ������.
    }

    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString(); // ���������� ���������� score ���������� �������� 0.
        settingsPopup.Close(); //�������� ����.
    }
    private void OnEnemyHit()
    {
        _score += 1;// ���������� ���������� score �� 1 � ����� �� ������ �������.
        scoreLabel.text = _score.ToString();
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    //scoreLabel.text = Time.realtimeSinceStartup.ToString(); // �� ������ ������ ����
    //}
    public void OnOpenSettings() // �����, ���������� ������� ��������.
    { 
        Debug.Log("open settings");
        settingsPopup.Open();
    }
}
