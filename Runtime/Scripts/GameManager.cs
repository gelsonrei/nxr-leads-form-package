using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Detecta Ctrl+Q pressionado e sai do aplicativo
                EncerrarAplicacao();
            }
        }
    }
    
    public void EncerrarAplicacao()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}