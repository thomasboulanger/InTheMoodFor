using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] WSInputReader _inputReader;
    void Start()
    {
        _inputReader.Init();
    }
}
