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
        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); //Объявляем, какой метод отвечает на событие ENEMY_HIT.
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit); //При разрушении объекта удаляйте подписчика, чтобы избежать ошибок.
    }

    void Start()
    {
        _score = 0;
        scoreLabel.text = _score.ToString(); // Присвоение переменной score начального значения 0.
        settingsPopup.Close(); //Закрытия окна.
    }
    private void OnEnemyHit()
    {
        _score += 1;// Увеличение переменной score на 1 в ответ на данное событие.
        scoreLabel.text = _score.ToString();
    }
    //// Update is called once per frame
    //void Update()
    //{
    //    //scoreLabel.text = Time.realtimeSinceStartup.ToString(); // не плохой таймер игры
    //}
    public void OnOpenSettings() // Метод, вызываемый кнопкой настроек.
    { 
        Debug.Log("open settings");
        settingsPopup.Open();
    }
}
