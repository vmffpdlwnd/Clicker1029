using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        if (!TryGetComponent<Camera>(out Camera cam))
            Debug.Log("cameraResolution.cs - Awake() - cam 참조 실패");

        Rect rt = cam.rect;

        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (currentSceneName == "GameScene1")
        {
            float scale_Height = ((float)Screen.width / Screen.height) / ((float)16f / 10f);
            float scale_Width = 1f / scale_Height;

            if (scale_Height < 1)
            {
                rt.height = scale_Height;
                rt.y = (1f - scale_Height) / 2;
            }
            else
            {
                rt.width = scale_Width;
                rt.x = (1f - scale_Width) / 2f;
            }

            cam.rect = rt;
        }
    }
}
