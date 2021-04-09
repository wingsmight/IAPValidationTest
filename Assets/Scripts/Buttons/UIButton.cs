using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIButton : MonoBehaviour
{
    protected Button button;
    protected AudioClip clickSound;


    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ActButton);
    }
    protected virtual void OnDestroy()
    {
        button.onClick.RemoveListener(ActButton);
    }

    protected abstract void OnClick();

    private void ActButton()
    {
        OnClick();
    }


    public Button Button => button;
}
