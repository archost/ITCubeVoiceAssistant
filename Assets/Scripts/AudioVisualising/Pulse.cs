using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField]
    private Material _targetMaterial;

    void Start()
    {
        if (_targetMaterial == null)
            Debug.LogError("Material is not assigned");
    }

    void Update()
    {
        Color color = _targetMaterial.color;
        color.a = AudioVisualizer._bandBuffer[7];
        _targetMaterial.SetColor("_Color", color);
    }
}