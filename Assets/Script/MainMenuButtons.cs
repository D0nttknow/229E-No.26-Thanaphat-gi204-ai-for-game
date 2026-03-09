using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // ใส่ชื่อซีนที่จะไปตอนกด Start (ต้องตรงกับชื่อใน Build Settings)
    [SerializeField] private string startSceneName = "GameScene";

    // เรียกจากปุ่ม Start (OnClick)
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(startSceneName))
        {
            SceneManager.LoadScene(startSceneName);
        }
        else
        {
            Debug.LogError("startSceneName is empty. Please set it in the Inspector.");
        }
    }

    // เรียกจากปุ่ม Exit (OnClick)
    public void ExitGame()
    {
        // เวลาเล่นใน Unity Editor ปุ่มนี้จะไม่ปิด Editor (เป็นปกติ)
        Application.Quit();

#if UNITY_EDITOR
        Debug.Log("ExitGame called (Application.Quit only works in a built player).");
#endif
    }
}