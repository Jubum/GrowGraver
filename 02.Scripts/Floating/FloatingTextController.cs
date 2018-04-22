using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {
    private static FloatingText popupText;
    private static FloatingText popupText2;
    private static FloatingText popupText3;
    private static FloatingSculpture popupSculpture;
    private static GameObject canvas;

    private static GameObject graver;

    public static void Initalize()
    {
        canvas = GameObject.Find("Canvas");
        graver = GameObject.Find("graver/Sculptures");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/Floating/TextParent");
        if (!popupText2)
            popupText2 = Resources.Load<FloatingText>("Prefabs/Floating/TextParent2");
        if (!popupText3)
            popupText3 = Resources.Load<FloatingText>("Prefabs/Floating/TextParent3");
        if (!popupSculpture)
            popupSculpture = Resources.Load<FloatingSculpture>("Prefabs/Floating/SculptureParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.SetAsFirstSibling();
        instance.SetText(text);
    }
    public static void CreateFloatingCriticalText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText2);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.SetAsFirstSibling();
        instance.SetText(text);
    }

    public static void CreateFloatingMoneyText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText3);
        instance.type = 2;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x, location.position.y));
        instance.transform.SetParent(canvas.transform, false);
        instance.SetLocation(location);
        instance.transform.SetAsFirstSibling();
        instance.SetText(text);
    }

    public static void CreateFloatingSclupture(Item item, Transform location)
    {
        FloatingSculpture instance = Instantiate(popupSculpture);
        instance.transform.SetParent(graver.transform, false);
        instance.transform.SetAsFirstSibling();
        instance.SetSprite(item);
    }
}
