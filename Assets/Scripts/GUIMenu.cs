using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour
{
    private byte window = 0;
    public GUIStyle style;
    bool fullScreens = true;
    bool volume = true;
    
    void Update()
    {
        Screen.fullScreen = fullScreens;
        Cursor.visible = true;
    }

    void OnGUI()
    {
        if (window == 0) //Главное меню
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 50, 220, 120), "Главное меню");

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 25), "Начать игру"))
            {
                Application.LoadLevel("Main");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 5, 200, 25), "Настройки"))
            {
                window = 1;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 40, 200, 25), "Выход"))
            {
                window = 2;
            }
        }

        if (window == 1) //Окно настроек
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 50, 220, 80), "Настройки");
            fullScreens = GUI.Toggle(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 30, 220, 30), fullScreens, " Оконный режим");
        
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 25), "Назад") || Input.GetKey("escape"))
            {
                window = 0;
            }
        }
        if (window == 2) //Подтверждение выхода
        {
            GUI.Box(new Rect(Screen.width / 2 - 110, Screen.height / 2 - 50, 220, 160), "Выход");
            GUI.Label(new Rect(Screen.width / 2 - 40, Screen.height / 2, 200, 25), "Уже выходите?");

            if (GUI.Button(new Rect(Screen.width / 2 - 105, Screen.height / 2 + 25, 100, 25), "Да"))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width / 2 + 5, Screen.height / 2 + 25, 100, 25), "Нет"))
            {
                window = 0;
            }
        }
    }

}