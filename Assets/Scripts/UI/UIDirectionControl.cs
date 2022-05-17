using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    public bool b_UseRelativeRotation = true;
    private Quaternion _relativeRotation;

    private void Start()
    {
        _relativeRotation = transform.parent.localRotation;
    }

    private void Update()
    {
        if (b_UseRelativeRotation)
            transform.rotation = _relativeRotation;
    }
}
