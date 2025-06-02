using UnityEngine;

public class OpenCloseButton : MonoBehaviour
{
    public GameObject panelToControl;
    public void OpenPanel()
    {
        if (panelToControl != null)
        {
            panelToControl.SetActive(true);
        }
    }

    public void ClosePanel()
    {
        if (panelToControl != null)
        {
            panelToControl.SetActive(false);
        }
    }
}
