using System.IO;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gaskellgames
{
	[CreateAssetMenu(fileName = "SceneDataList_SO", menuName = "Gaskellgames/SceneDataList_SO")]
    public class SceneDataList_SO : ScriptableObject
    {
        #region Variables

        [SerializeField]
        private List<SceneData> buildSceneData;

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Getter / Setter

        /// <summary>
        /// List of all scenes in the build, in the form of SceneData
        /// </summary>
        public List<SceneData> BuildSceneData
        {
            get => buildSceneData;
            private set => buildSceneData = value;
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        #region EditorOnly

        [SerializeField]
        private List<SceneData> projectSceneData;

        /// <summary>
        /// List of all scenes in the project, in the form of SceneData
        /// </summary>
        public List<SceneData> ProjectSceneData
        {
            get => projectSceneData;
            private set => projectSceneData = value;
        }
        
        public void Editor_UpdateSceneDataLists()
        {
            // update list of all scenes in project
            projectSceneData = new List<SceneData>();
            string[] filePaths = Directory.GetFiles("Assets", "*.unity", SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
            {
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(filePath);
                if (sceneAsset == null) { continue; }
                SceneData sceneData = new SceneData(sceneAsset);
                if (!projectSceneData.Contains(sceneData))
                {
                    projectSceneData.Add(sceneData);
                }
            }

            // update list of all scenes in build
            buildSceneData = new List<SceneData>();
            foreach (EditorBuildSettingsScene buildScene in EditorBuildSettings.scenes)
            {
                if (buildScene.enabled)
                {
                    SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(buildScene.path);
                    buildSceneData.Add(new SceneData(sceneAsset));
                }
            }
        }

        #endregion
#endif

    } // class end
}
