using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DAPathEditorData : ScriptableObject
{
    public DAString variable;
}

public class DAPathEditor : EditorWindow
{
    private DAPathEditorData _data;
    private System.Action _onDestroy;

    //UI
    private TextField _pathField;
    private TextField _formattedPathField;

    public void Open(Rect rect, DAString variable, System.Action onDestroy = null)
    {
        _onDestroy = onDestroy;
        ShowAsDropDown(rect, new Vector2(350f, 110f));
        Setup(variable);
        CreateUI();
    }

    private void OnDestroy()
    {
        _onDestroy?.Invoke();
        Undo.ClearUndo(_data);
    }

    private void Setup(DAString variable)
    {
        _data = CreateInstance<DAPathEditorData>();
        _data.variable = variable;
        StyleSheet pathEditorStyleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{DAUtils.GetPathOfScriptableObject(this)}/PathEditor.uss");
        rootVisualElement.styleSheets.Add(pathEditorStyleSheet);
        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void CreateUI()
    {
        rootVisualElement.AddToClassList("root");

        //Title
        var titleLabel = new Label("Edit Path");
        titleLabel.AddToClassList("title");
        rootVisualElement.Add(titleLabel);

        //Special Folders
        Button specialFoldersButton = new Button();
        specialFoldersButton.AddToClassList("special-folders");
        specialFoldersButton.text = "Special Folders";
        specialFoldersButton.clickable.clicked += ShowSpecialFolders;
        rootVisualElement.Add(specialFoldersButton);

        //Path
        _pathField = new TextField();
        _pathField.label = "Path";
        _pathField.isDelayed = true;
        _pathField.RegisterValueChangedCallback((evt) =>
        {
            Undo.RecordObject(_data, "Path");
            _data.variable.value = evt.newValue;
            _formattedPathField.value = DAPath.FormatPath(evt.newValue);
        });
        rootVisualElement.Add(_pathField);

        //Formatted Path
        _formattedPathField = new TextField("Formatted Path");
        _formattedPathField.AddToClassList("formatted-path-field");
        _formattedPathField.isReadOnly = true;
        rootVisualElement.Add(_formattedPathField);

        RefreshValues();
    }

    private void ShowSpecialFolders()
    {
        var menu = new GenericMenu();
        foreach (var folder in DAPath.specialFolders)
        {
            menu.AddItem(new GUIContent(folder.Value.ToString()), false, () =>
            {
                Undo.RecordObject(_data, "Path");
                _data.variable.value = folder.Key;
                RefreshValues();
            });
        }
        menu.ShowAsContext();
    }

    private void RefreshValues()
    {
        _pathField.SetValueWithoutNotify(_data.variable.value);
        _formattedPathField.value = DAPath.FormatPath(_data.variable.value);
        _formattedPathField.tooltip = _formattedPathField.value;
    }

    private void OnUndoRedo()
    {
        RefreshValues();
    }
}
