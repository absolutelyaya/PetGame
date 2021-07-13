using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using System;
using TMPro;

public class TextTyper
{
    public event Action<string> KeyboardCallback;
    public event Action FinishExecution;

    string curText = string.Empty;
    bool finished = false;
    bool canceled = false;

    readonly bool forceUppercase = false;
    readonly int typeSpeed;
    readonly TextMeshProUGUI target;

    /// <summary>
    /// Creates a new TextTyper.
    /// </summary>
    /// <param name="_target">The target TextMeshProUGUI.</param>
    /// <param name="_typeSpeed">How quick to type (Chars per Second).</param>
    /// <param name="_onFinishExecution">Will Fire when a ⏹ is reached in the input text (The TextTyper will also stop Updating the target.).</param>
    /// <param name="_keyboardCallback">Will fire when KeyboardInput is submitted.</param>
    public TextTyper(TextMeshProUGUI _target, int _typeSpeed, Action _onFinishExecution = null, Action<string> _keyboardCallback = null)
    {
        target = _target;
        typeSpeed = _typeSpeed;

        if(_onFinishExecution != null)
            FinishExecution += _onFinishExecution;
        if (_keyboardCallback != null)
            KeyboardCallback += _keyboardCallback;

        UpdateText();
        Application.quitting += () =>
        {
            canceled = true;
        };
    }

    /// <summary>
    /// Creates a new TextTyper.
    /// </summary>
    /// <param name="_target">The target TextMeshProUGUI.</param>
    /// <param name="_typeSpeed">How quick to type (Chars per Second).</param>
    /// <param name="_forceUppercase">Force Written text to be Uppercase.</param>
    /// <param name="_onFinishExecution">Will Fire when a ⏹ is reached in the input text (The TextTyper will also stop Updating the target.).</param>
    /// <param name="_keyboardCallback">Will fire when KeyboardInput is submitted.</param>
    public TextTyper(TextMeshProUGUI _target, int _typeSpeed, bool _forceUppercase, Action _onFinishExecution = null, Action<string> _keyboardCallback = null)
    {
        target = _target;
        typeSpeed = _typeSpeed;
        forceUppercase = _forceUppercase;

        if (_onFinishExecution != null)
            FinishExecution += _onFinishExecution;
        if (_keyboardCallback != null)
            KeyboardCallback += _keyboardCallback;

        UpdateText();
        Application.quitting += () =>
        {
            canceled = true;
        };
    }

    /// <summary>
    /// Changes the target text to the given string.
    /// Special Chars:
    /// ☑[key|queue] Opens the Keyboard. The callback will be in the format "key|Input". The same key that is put in will come out.
    /// ⏹ Stops updating the text. The OnFinishedExecution Event is fired. Used at the end of Sequences, like the Adoption procedure.
    /// ∛ A line break.
    /// ㊡ Pause for 1 Second.
    /// . Pause for 0.2 Seconds, then write the dot.
    /// </summary>
    /// <param name="text">The given string.</param>
    public void SetText(string text)
    {
        curText = forceUppercase ? text.ToUpper() : text;
    }

    async void UpdateText()
    {
        string curTask = string.Empty;
        bool UpdatePending = false;
        while (!finished)
        {
            if (canceled)
                return;
            await Task.Delay(Mathf.RoundToInt(Time.deltaTime * 1000));
            if (curTask != curText)
            {
                UpdatePending = true;
                curTask = curText;
            }
            if (curTask == string.Empty && target.text.Length > 0 && UpdatePending)
            {
                while (target.text.Length > 0)
                {
                    if (canceled)
                        return;
                    await Task.Delay((int)(1f / (typeSpeed * 2) * 1000));
                    if (target.text.EndsWith("<br>"))
                        target.text = target.text.Substring(0, target.text.Length - 4);
                    else
                        target.text = target.text.Substring(0, target.text.Length - 1);
                }
                UpdatePending = false;
            }
            else if (UpdatePending)
            {
                if (canceled)
                    return;
                for (int i = 0; i < curTask.Length; i++)
                {
                    if (canceled)
                        return;
                    await Task.Delay((int)(1f / typeSpeed * 1000));
                    switch (curTask[i])
                    {
                        case '☑':
                            string q = "out|";
                            if(curTask[i + 1] == '[')
                            {
                                q = curTask.Split(']')[0].Substring(i + 2);
                                curTask = curTask.Substring(0, i);
                            }
                            await InstantiateKeyboard(q);
                            break;
                        case '⏹':
                            finished = true;
                            break;
                        case '∛':
                            target.text += "<br>";
                            break;
                        case '㊡':
                            await Task.Delay(1000);
                            break;
                        case '.':
                            target.text += curTask[i];
                            await Task.Delay(200);
                            break;
                        default:
                            target.text += curTask[i];
                            break;
                    }
                    if (curText == string.Empty)
                        break;
                }
                UpdatePending = false;
            }
        }
        FinishExecution?.Invoke();
    }


    async Task<Task> InstantiateKeyboard(string question)
    {
        SceneManager.LoadScene("Keyboard", LoadSceneMode.Additive);
        while (!Keyboard.Reference)
            await Task.Delay(Mathf.RoundToInt(Time.deltaTime * 1000)); //Waiting for the Keyboard to be fully loaded...
        Keyboard.Reference.SetQuestion(question);
        Keyboard.Reference.ConfirmEvent += KeyboardCallback;
        curText = string.Empty;
        return Task.CompletedTask;
    }
}
