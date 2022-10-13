using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    enum LoadMode { None, Awake, Start, LateUpdate }

    [SerializeField] LoadMode _mode;

    [SerializeField, Scene] int _scene;

    private void Awake() { if (_mode == LoadMode.Awake) SceneManager.LoadScene(_scene); }
    private void Start() { if (_mode == LoadMode.Start) SceneManager.LoadScene(_scene); }
    private void LateUpdate() { if (_mode == LoadMode.LateUpdate) SceneManager.LoadScene(_scene); }

    public void Load() { SceneManager.LoadScene(_scene); }
}
