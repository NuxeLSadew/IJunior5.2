using UnityEngine;

public class LerpedColorChanger : MonoBehaviour
{
    [SerializeField] private Color _targetColor;

    private Renderer _renderer;
    private int _baseMaterialIndex = 0;
    private Material _material;
    private Color _baseColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.materials[_baseMaterialIndex];
        _baseColor = _material.color;
       
    }

    public void ChangeColor(float value)
    {
        _material.color = Color.Lerp(_targetColor, _baseColor, value);
    }

    public void ChangeColorReversed(float value)
    {
        _material.color = Color.Lerp(_baseColor, _targetColor, value);
    }
}
