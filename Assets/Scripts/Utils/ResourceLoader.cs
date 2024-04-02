using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ResourceLoader : MonoBehaviour
{
    /*
    public static string circlePath = "Shapes2D Sprites/Circle";
    public static string trianglePath = "Shapes2D Sprites/Triangle";
    public static string squarePath = "Shapes2D Sprites/Rectangle";
    */

    public static string circlePath = "ShapeSprites/CircleSprite";
    public static string trianglePath = "ShapeSprites/TriangleSprite";
    public static string squarePath = "ShapeSprites/SquareSprite";
    // Assets/Resources/ShapeSprites/CircleSprite.png
    public static string circleDefaultColor = "#F68CBD";
    public static string triangleDefaultColor = "#63af57";
    public static string squareDefaultColor = "#3a90d0";

    public static Sprite GetSprite(Shapes shape){
        Sprite tex = null;
        switch(shape) {
            case Shapes.Circle:
                tex = Resources.Load<Sprite>(circlePath);
                break;
            case Shapes.Square:
                tex = Resources.Load<Sprite>(squarePath);
                break;
            case Shapes.Triangle:
                tex = Resources.Load<Sprite>(trianglePath);
                break;
        }
        
        // _image.sprite = tex;
        return tex;
    }

    public static string GetDefaultColour(Shapes shape){
        string colourHex = null;
        switch(shape) {
            case Shapes.Circle:
                colourHex = circleDefaultColor;
                break;
            case Shapes.Square:
                colourHex = squareDefaultColor;
                break;
            case Shapes.Triangle:
                colourHex = triangleDefaultColor;
                break;
        }
        
        return colourHex;
    }

    public static Color GetHexColour(string hexString){
        Color colorToReturn = Color.white;
        ColorUtility.TryParseHtmlString(hexString, out colorToReturn);
        return colorToReturn;
    }
}
