namespace Anvil
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class CameraResolution : MonoBehaviour   //Data Field
    {
        private enum LengthWidth { Length, Width }

        [SerializeField]
        private Camera playerCamera = null;
        [SerializeField]
        private LengthWidth lengthWidth = LengthWidth.Length;
    }

    public partial class CameraResolution : MonoBehaviour   //Function Field
    {
        private void Awake()
        {
            if (lengthWidth == LengthWidth.Length)
                Length();
            else if (lengthWidth == LengthWidth.Width)
                Width();

        }

        private void Length()
        {
            Rect rect = playerCamera.rect;
            float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (가로 / 세로)
            float scalewidth = 1f / scaleheight;
            if (scaleheight < 1)
            {
                rect.height = scaleheight;
                rect.y = (1f - scaleheight) / 2f;
            }
            else
            {
                rect.width = scalewidth;
                rect.x = (1f - scalewidth) / 2f;
            }
            playerCamera.rect = rect;
        }
        private void Width()
        {
            Rect rect = playerCamera.rect;
            float scaleheight = ((float)Screen.width / Screen.height) / ((float)9 / 16); // (가로 / 세로)
            float scalewidth = 1f / scaleheight;
            if (scaleheight < 1)
            {
                rect.height = scaleheight;
                rect.y = (1f - scaleheight) / 2f;
            }
            else
            {
                rect.width = scalewidth;
                rect.x = (1f - scalewidth) / 2f;
            }
            playerCamera.rect = rect;
        }

        private void OnPreCull() => GL.Clear(true, true, Color.black);
    }
}