using UnityEngine;

public static class ExtensionGameObject
{
    //Method for giving more safety. 
    public static T GetSafeComponent<T>(this GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError("The component " + typeof(T) + " doesn't exist in the GameObject: " + obj.name);
        }
        return component;
    }
}
