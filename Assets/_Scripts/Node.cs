using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] Color newHighlightColor;
    [SerializeField] SpriteRenderer highlightSprite;

    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = highlightSprite.color;
    }
    private void OnMouseEnter()
    {
        highlightSprite.color = newHighlightColor;
    }
    private void OnMouseExit()
    {
        highlightSprite.color = _defaultColor;
    }
}
