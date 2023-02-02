using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_Debugger : MonoBehaviour
{
    private static S_Debugger instance;
    private List<DEBUG_MESSAGE> messageStack;
    private Dictionary<string, DEBUG_MESSAGE> objectStack;
    private Dictionary<string, UnityAction> actionStack;
    public Color defaultColor = Color.white;
    private bool toogle;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            messageStack = new List<DEBUG_MESSAGE>();
            objectStack = new Dictionary<string, DEBUG_MESSAGE>();
            actionStack = new Dictionary<string, UnityAction>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            toogle = !toogle;
        }
    }


    public static void Log(string message, Color color, float time = 2f)
    {
        var debugMessage = new DEBUG_MESSAGE(message, color);
        instance.messageStack.Add(debugMessage);
        instance.StartCoroutine(instance.DestroyMessage(debugMessage, time));
        Debug.Log(message);
    }
    public static void Log(string message, float time = 2f)
    {
        S_Debugger.Log(message, instance.defaultColor, time);    
    }
    public static void UpdatableLog(string id, object message, Color color)
    {
        var debugMessage = new DEBUG_MESSAGE(id + ": " + message.ToString(), color);
        if(!instance.objectStack.ContainsKey(id))
        {
            instance.objectStack.Add(id, debugMessage);
        }
        instance.objectStack[id] = debugMessage;
    }
    public static void UpdatableLog(string id, object message)
    {
        S_Debugger.UpdatableLog(id, message, instance.defaultColor);
    }
    
    public static void AddButton(string name, UnityAction action)
    {
        if (!instance.actionStack.ContainsKey(name))
        {
            instance.actionStack.Add(name, action);
        }
        instance.actionStack[name] = action;
    }

    private void OnGUI()
    {
        if(toogle)
        {
            return;
        }
        GUILayout.BeginHorizontal("box");
        GUILayout.BeginVertical();
        foreach (var id in objectStack.Keys)
        {
            GUI.color = objectStack[id].color;
            GUILayout.Label(objectStack[id].message);
        }
        foreach (var message in messageStack)
        {
            GUI.color = message.color;
            GUILayout.Label(message.message);
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        foreach (var item in actionStack)
        {
            if (GUILayout.Button(item.Key))
            {
                item.Value.Invoke();
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }

    private IEnumerator DestroyMessage(DEBUG_MESSAGE message, float time = 2f)
    {
        yield return new WaitForSeconds(time);
        messageStack.Remove(message);
    }
}
public struct DEBUG_MESSAGE
{
    public string message;
    public Color color;

    public DEBUG_MESSAGE(string message, Color color)
    {
        this.message = message;
        this.color = color;
    }
}
