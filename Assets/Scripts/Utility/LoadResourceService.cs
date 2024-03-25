using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Utility
{
    public static class LoadResourceService
    {
        private static readonly Dictionary<string, Object> s_resourceCached = new();

        #region API

        public static T LoadCsv<T>() where T : ScriptableObject
        {
            var resourcePath = typeof(T).FullName;

            var asset = LoadAsset<T>(resourcePath);
            return asset;
        }
        

        public static T LoadAsset<T>(string resourcePath) where T : Object
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));

            var asset = LoadResourceFromAddressable<T>(resourcePath);
            if (asset == null)
                asset = LoadResourceFromResource<T>(resourcePath);

            return asset;
        }

        #endregion

        #region Helper

        private static T LoadResourceFromAddressable<T>(string resourcePath) where T : Object
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));

            if (s_resourceCached.TryGetValue(resourcePath, out Object value))
            {
                return value as T;
            }

            try
            {
                var handler = Addressables.LoadAssetAsync<T>(resourcePath);
                handler.WaitForCompletion();

                var result = handler.Result;
                if (result == null)
                {
                    return null;
                }

                s_resourceCached.TryAdd(resourcePath, result);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private static T LoadResourceFromResource<T>(string resourcePath) where T : Object
        {
            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));

            if (s_resourceCached.TryGetValue(resourcePath, out Object value))
            {
                return value as T;
            }

            var result = Resources.Load<T>(resourcePath);
            if (result == null)
                return null;

            s_resourceCached.TryAdd(resourcePath, result);
            return result;
        }

        public static async UniTask PreloadCsv<T>() where T : ScriptableObject
        {
            var resourcePath = typeof(T).FullName;

            if (resourcePath == null)
                throw new ArgumentNullException(nameof(resourcePath));

            AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(resourcePath);

            var result = await loadHandle.ToUniTask();
            if (result == null)
                return;

            s_resourceCached.TryAdd(resourcePath, result);
        }

        #endregion
    }
}