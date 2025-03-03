using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite_render;
    [SerializeField] private Color color;
    public void SetColor()
    {
        sprite_render.color = color;
    }
}
