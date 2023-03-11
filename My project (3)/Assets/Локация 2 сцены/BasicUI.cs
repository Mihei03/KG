using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 40, 20), "Test")) // Параметры: положение по оси X, положение по оси Y, ширина, высота, текстовая подпись.
                        Debug.Log("Test button");
    }
}
