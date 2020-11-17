using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Compilation;
using System.Linq;

namespace Kirbyrawr.DivineAutomatization
{
    public class DAEditor : EditorWindow
    {
        public static DAEditor Instance { get; private set; }

        public DAGraphView graphView;
        public DABlackboard blackboard;
        public DANodeSearchPopup searchNodePopup;
        public DAEnumSearchPopup searchEnumPopup;
        public DAPropertiesSearchPopup searchPropertiesPopup;
        public DAEdgeConnectorListener edgeConnectorListener = new DAEdgeConnectorListener();
        public Dictionary<string, StyleSheet> Styles { get; private set; }

        private DAGraphObject _currentGraphObject;
        private string _graphObjectPath;

        public static void Init(DAGraphObject graphObject)
        {
            Undo.ClearAll();
            var editor = GetWindow<DAEditor>();
            editor.Load(graphObject);
        }

        private void Setup()
        {
            SetInstance();
            LoadStyles();
            CreateGraphView();
            CreateBlackboard();
            CreateToolbar();
            CreateSearchNodePopup();
            CreateSearchEnumPopup();
            CreateSearchPropertyPopup();
            if (_currentGraphObject != null)
            {
                Load(_currentGraphObject);
            }
        }

        private void OnEnable()
        {
            Setup();
        }

        private void OnDestroy()
        {
            Instance = null;
            DestroyImmediate(graphView.GraphObject);
        }

        private void SetInstance()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                DestroyImmediate(Instance);
            }
        }

        private void LoadStyles()
        {
            MonoScript ms = MonoScript.FromScriptableObject(this);
            string relativePath = AssetDatabase.GetAssetPath(ms).Replace(GetType().Name + ".cs", "");

            string stylesPath = $"{relativePath}EditorResources/Styles";

            Styles = new Dictionary<string, StyleSheet>();
            Styles.Add("Graph", AssetDatabase.LoadAssetAtPath<StyleSheet>($"{stylesPath}/Graph.uss"));
            Styles.Add("Node", AssetDatabase.LoadAssetAtPath<StyleSheet>($"{stylesPath}/Node.uss"));
        }

        private void CreateGraphView()
        {
            graphView = new DAGraphView(this);

            EventCallback<GeometryChangedEvent> callback = null;
            callback = ((evt) =>
            {
                graphView.UnregisterCallback<GeometryChangedEvent>(callback);
                graphView.FrameAll();
            });

            graphView.RegisterCallback<GeometryChangedEvent>(callback);

            graphView.styleSheets.Add(Styles["Graph"]);
        }

        private void CreateBlackboard()
        {
            blackboard = new DABlackboard(graphView);
            graphView.Add(blackboard);
        }

        private void CreateToolbar()
        {
            Toolbar toolbar = new Toolbar();

            ToolbarButton loadButton = new ToolbarButton(OpenLoadDialog);
            loadButton.text = "Load";
            toolbar.Add(loadButton);

            ToolbarButton saveButton = new ToolbarButton(() => Save(_graphObjectPath));
            saveButton.text = "Save";
            toolbar.Add(saveButton);

            ToolbarButton saveAsButton = new ToolbarButton(OpenSaveAsDialog);
            saveAsButton.text = "Save As..";
            toolbar.Add(saveAsButton);

            rootVisualElement.Insert(0, toolbar);
        }

        private void CreateSearchNodePopup()
        {
            if (searchNodePopup != null) { DestroyImmediate(searchNodePopup); }
            searchNodePopup = CreateInstance<DANodeSearchPopup>();
            searchNodePopup.Setup(this);
        }

        private void CreateSearchEnumPopup()
        {
            if (searchEnumPopup != null) { DestroyImmediate(searchEnumPopup); }
            searchEnumPopup = CreateInstance<DAEnumSearchPopup>();
            searchEnumPopup.Setup(this);
        }

        private void CreateSearchPropertyPopup()
        {
            if (searchPropertiesPopup != null) { DestroyImmediate(searchPropertiesPopup); }
            searchPropertiesPopup = CreateInstance<DAPropertiesSearchPopup>();
            searchPropertiesPopup.Setup(this);
        }

        private void OpenSaveAsDialog()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save As...", "", "asset", "");
            if (!string.IsNullOrEmpty(path))
            {
                Save(path);
            }
        }

        private void OpenLoadDialog()
        {
            string path = EditorUtility.OpenFilePanelWithFilters("Load", "Assets/", new string[2] { "Asset", "asset" });
            if (!string.IsNullOrEmpty(path))
            {
                string relativePath = path.Replace(Application.dataPath, "Assets");
                Load(AssetDatabase.LoadAssetAtPath<DAGraphObject>(relativePath));
            }
        }

        private void Save(string path)
        {
            graphView.Serialize();
            var instance = Instantiate(graphView.GraphObject);
            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            Selection.activeObject = instance;
        }

        private void Load(DAGraphObject graphObject)
        {
            var path = AssetDatabase.GetAssetPath(graphObject);
            if (path != null)
            {
                _graphObjectPath = path;
            }

            var graphObjectInstance = Instantiate(graphObject);
            _currentGraphObject = graphObjectInstance;
            graphView.GraphObject = graphObjectInstance;
            graphView.GraphObject.Setup(graphView);
            graphView.Clean();
            graphView.Deserialize();
        }
    }
}