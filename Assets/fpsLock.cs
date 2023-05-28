using UnityEngine;
[ExecuteAlways]
public class fpsLock : MonoBehaviour
{
    [SerializeField] int fps;
    [ExecuteAlways]
    void Start()
    {
        Application.targetFrameRate = fps;
    }
}
