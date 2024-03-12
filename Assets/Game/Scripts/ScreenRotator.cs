using UnityEngine;

public class ScreenRotator : MonoBehaviour
{
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        
        Screen.orientation = ScreenOrientation.AutoRotation;
        
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
}
