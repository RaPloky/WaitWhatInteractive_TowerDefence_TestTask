using UnityEngine;

public class NodeUI : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private Node _target;

    public void SetTarget(Node target)
    {
        UI.SetActive(true);
        _target = target;
        transform.position = target.transform.position;
    }
    public void Hide()
    {
        UI.SetActive(false);
    }
}
