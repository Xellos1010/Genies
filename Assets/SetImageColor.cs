using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetImageColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imageComponent
    {
        get
        {
            if(ImageComponent == null)
                ImageComponent = GetComponent<Image>();
            return ImageComponent;

        }
    }

    private Image ImageComponent;

    public void SetImageColorTo(Color imageColor)
    {
        imageComponent.color = imageColor;
    }
}
