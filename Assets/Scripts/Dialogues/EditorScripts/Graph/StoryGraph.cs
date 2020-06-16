#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class StoryGraph : EditorWindow
{
    private string _fileName = "New Narrative";
    private string _path = "Assets/Resources/Quests";

    private StoryGraphView _graphView;
    private DialogueContainer _dialogueContainer;

    [MenuItem("Dialogues/Graph")]
    public static void CreateGraphViewWindow()
    {
        var window = GetWindow<StoryGraph>();
        window.titleContent = new GUIContent("Narrative Graph");
    }

    public void CreateGraphEditViewWindow(string fileName, string path)
    {
        _fileName = fileName;
        _path = path;
        var window = GetWindow<StoryGraph>();
        window.titleContent = new GUIContent("Narrative Graph");
    }

    private void ConstructGraphView()
    {
        _graphView = new StoryGraphView(this)
        {
            name = "Narrative Graph",
        };
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        fileNameTextField.style.width = 400;
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) {text = "Save Data"});

        toolbar.Add(new Button(() => RequestDataOperation(false)) {text = "Load Data"});
        toolbar.Add(new Button(() => _graphView.CreateNewDialogueNodePlayer("Dialogue Node", _graphView.LastNodePosition)) {text = "Player Node"});
        toolbar.Add(new Button(() => _graphView.CreateNewDialogueNodeNPC("Dialogue Node", _graphView.LastNodePosition)) {text = "NPC Node"});
        rootVisualElement.Add(toolbar);
    }

    public void RequestDataOperation(bool save)
    {
        if (!string.IsNullOrEmpty(_fileName))
        {
            var saveUtility = GraphSaveUtility.GetInstance(_graphView);
            if (save)
                saveUtility.SaveGraph(_fileName, _path);
            else
                saveUtility.LoadNarrative(_fileName, _path);
        }
        else
        {
            EditorUtility.DisplayDialog("Invalid File name", "Please Enter a valid filename", "OK");
        }
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMiniMap();
        GenerateBlackBoard();
    }

    private void GenerateMiniMap()
    {
        var miniMap = new MiniMap {anchored = true};
        var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x - 10, 30));
        miniMap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
        _graphView.Add(miniMap);
    }

    private void GenerateBlackBoard()
    {
        var blackboard = new Blackboard(_graphView);
        blackboard.Add(new BlackboardSection {title = "Exposed Variables"});
        blackboard.addItemRequested = _blackboard =>
        {
            _graphView.AddPropertyToBlackBoard(ExposedProperty.CreateInstance(), false);
        };
        blackboard.editTextRequested = (_blackboard, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField) element).text;
            if (_graphView.ExposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog("Error", "This property name already exists, please chose another one.",
                    "OK");
                return;
            }

            var targetIndex = _graphView.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            _graphView.ExposedProperties[targetIndex].PropertyName = newValue;
            ((BlackboardField) element).text = newValue;
        };
        blackboard.SetPosition(new Rect(10,30,200,300));
        _graphView.Add(blackboard);
        _graphView.Blackboard = blackboard;
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
#endif