#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gaskellgames.SceneManagement
{
    public class SceneDataList_AutoUpdater : AssetPostprocessor, ISerializationCallbackReceiver
    {
        #region AssetPostprocessor

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            UpdateSceneDataList();
        }

        #endregion

        //----------------------------------------------------------------------------------------------------

        #region ISerializationCallbackReceiver

        public void OnBeforeSerialize()
        {
            UpdateSceneDataList();
        }

        public void OnAfterDeserialize()
        {
            // blank
        }

        #endregion
        
        //----------------------------------------------------------------------------------------------------

        #region Private Functions

        private static void UpdateSceneDataList()
        {
            // get reference to list
            SceneDataList_SO sceneDataList = GetSceneDataListSO();
            
            // update scene context list
            if (sceneDataList != null)
            {
                sceneDataList.Editor_UpdateSceneDataLists();
            }
        }
        
        private static SceneDataList_SO GetSceneDataListSO()
        {
            string saveLocation = "Assets/Gaskellgames/Scene Controller/Resources/Data";
            string fileName = "SceneDataList";
            
            
            List<SceneDataList_SO> sceneDataLists = new List<SceneDataList_SO>();
            string[] sceneDataListGUIDs = AssetDatabase.FindAssets($"t:{nameof(SceneDataList_SO)}", new[] { "Assets/" });
            foreach (string guid in sceneDataListGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                SceneDataList_SO asset = AssetDatabase.LoadAssetAtPath<SceneDataList_SO>(assetPath);
                if (asset != null)
                {
                    sceneDataLists.Add(asset);
                }
            }
            int count = sceneDataLists.Count;
            
            // check if duplicate files exist: delete all but one
            if (1 < count)
            {
                Debug.Log(count + " SceneDataList's in files:\nDeleting " + (count - 1) + " lists.");
                for (int i = 1; i < count; i++)
                {
                    File.Delete(AssetDatabase.GetAssetPath(sceneDataLists[i]));
                }
            }
            
            // check if file exists: if not create one
            if (count == 0)
            {
                Debug.Log(count + " SceneDataList's in files:\nCreating new list.");
                SceneDataList_SO instance = ScriptableObject.CreateInstance<SceneDataList_SO>();
                instance.name = fileName;
                AssetDatabase.CreateAsset(instance, saveLocation + fileName + ".asset");
                AssetDatabase.SaveAssets();
            }
            
            AssetDatabase.Refresh();
            string sceneDataListPath = AssetDatabase.GUIDToAssetPath(sceneDataListGUIDs[0]);
            return AssetDatabase.LoadAssetAtPath<SceneDataList_SO>(sceneDataListPath);
        }

        #endregion
        
    } // class end
} 
#endif