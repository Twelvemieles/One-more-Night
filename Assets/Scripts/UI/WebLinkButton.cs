using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLinkButton : MonoBehaviour
{
    [SerializeField] private string link;
    public void SetURL(string url)
    {
        link = url;
    }
    public void GoToLink()
    {
        if(!string.IsNullOrEmpty(link))
        {
            Application.OpenURL(link);
        }
    }
}
