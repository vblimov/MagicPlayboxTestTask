#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Ingosstrakh.ResourcesLoader
{
    public static class AssetDatabaseUtils
    {
        public static string GetSelectionObjectPath()
        {
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }
            return path;
        }

        public static object GetAssetOfType(string name, string typeName = null)
        {
            var guids = AssetDatabase.FindAssets(name + " t:" + typeName);
            if (guids.Length == 0)
                return null;
            var guid = guids[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            foreach (var o in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                return o;
            }
            return null;
        }

        public static T[] GetAssetsAtPath<T>(Object @object) where T : Object
        {
            var objectLocalPath = AssetDatabase.GetAssetPath(@object);
            return GetAssetsAtPath<T>(objectLocalPath);
        }

        public static T[] GetAssetsAtPath<T>(string path) where T : Object
        {
            var al = new ArrayList();
            var pathInsideAssets = Application.dataPath;
            var assetsPathIndex = pathInsideAssets.LastIndexOf('/');
            var dataPathOutsideAssets = pathInsideAssets.Substring(0, assetsPathIndex);

            var localFolderIndex = path.LastIndexOf('/');
            var localFolderPath = path.Substring(0, localFolderIndex + 1);

            var systemPath = dataPathOutsideAssets + '/' + localFolderPath;
            Debug.Log(systemPath);
            var fileEntries = Directory.GetFiles(systemPath);
            foreach (var fileName in fileEntries)
            {
                var index = fileName.LastIndexOf('/');
                var localPath = localFolderPath;

                if (index > 0)
                    localPath += fileName.Substring(index);

                var t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

                if (t != null)
                    al.Add(t);
            }

            var result = new T[al.Count];
            for (var i = 0; i < al.Count; i++)
            {
                result[i] = (T)al[i];
            }

            return result;
        }

        public static T GetAssetOfTypeExact<T>(string name) where T : Object
        {
            var assets = AssetDatabaseUtils.GetAssetsOfType<T>();
            return assets?.FirstOrDefault(b => string.Equals(name, b.name));
        }

        public static T GetAssetOfType<T>(string name, string subAssetName = null, System.Type mainType = null) where T : class
        {
            mainType ??= typeof(T);
            var guids = AssetDatabase.FindAssets(name + " t:" + mainType.Name);
            if (guids.Length == 0)
                return null;
            var guid = guids[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            foreach (var o in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                if (!(o is T res))
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(subAssetName) && subAssetName != o.name)
                {
                    continue;
                }
                return res;
            }
            return default(T);
        }

        public static Object GetAssetOfTypeByGuid(string guid, System.Type type, string subassetName = null)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(subassetName))
            {
                return AssetDatabase.LoadAssetAtPath(path, type);
            }

            return AssetDatabase.LoadAllAssetsAtPath(path).FirstOrDefault(o => o.name == subassetName);
        }

        public static T GetAssetOfTypeByGuid<T>(string guid, string subAssetName = null, System.Type mainType = null) where T : class
        {
            mainType ??= typeof(T);
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            if (mainAsset is T res && (string.IsNullOrEmpty(subAssetName) || subAssetName == mainAsset.name))
            {
                return res;
            }
            foreach (var o in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                res = o as T;
                if (res == null)
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(subAssetName) && subAssetName != o.name)
                {
                    continue;
                }
                return res;
            }
            return default(T);
        }

        public static Object[] GetAllAssetByGuid(string guid)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAllAssetsAtPath(path);
        }

        public static T GetAssetOfType<T>(bool unique = false) where T : class
        {
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).FullName);
            if (guids.Length == 0)
                return null;
            if (guids.Length > 1 && unique)
            {
                var paths = guids.Select(AssetDatabase.GUIDToAssetPath).Aggregate("", (current, assetPath) => current + (assetPath + "\n"));
                throw new System.ArgumentException("Has multiple objects with this type: \n" + paths);
            }
            var guid = guids[0];
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
        }

        public static T[] GetAssetsOfType<T>() where T : class
        {
            if (typeof(UnityEngine.Component).IsAssignableFrom(typeof(T)))
            {
                var guidsGo = AssetDatabase.FindAssets("t:Prefab");
                var l = new List<T>();
                foreach (var g in guidsGo)
                {
                    var path = AssetDatabase.GUIDToAssetPath(g);
                    var t = (AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject)?.GetComponent<T>();
                    if (t != null)
                    {
                        l.Add(t);
                    }
                }
                return l.ToArray();
            }

            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            if (guids.Length == 0)
                return null;

            var i = 0;
            var res = new T[guids.Length];
            foreach (var g in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(g);
                var t = AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
                res[i] = t;
                i++;
            }
            return res;
        }

        public static T Duplicate<T>(T originalAsset, string path, bool overrideIfExists = true) where T : Object
        {
            var originalPath = AssetDatabase.GetAssetPath(originalAsset).Replace(@"/", @"\");
            if (File.Exists(originalPath))
            {
                if (!File.Exists(path) || overrideIfExists)
                {
                    AssetDatabase.CopyAsset(originalPath, path);
                    AssetDatabase.Refresh();
                    return (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
                }
                else
                {
                    Debug.LogError($"Could not override asset at path {path}");
                }
            }
            else
            {
                Debug.LogError($"Asset at path {originalPath} doesn't exist");
            }
            return null;
        }

        public static string GetOrCreateDirectory(string directory, string folder)
        {
            var fullPath = $"{directory}/{folder}";

            if (!AssetDatabase.IsValidFolder(fullPath))
            {
                AssetDatabase.CreateFolder(directory, folder);
                AssetDatabase.Refresh();
            }
            return fullPath;
        }
    }
}

#endif