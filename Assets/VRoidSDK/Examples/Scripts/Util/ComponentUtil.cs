using UnityEngine;

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace VRoidSDK.Example
{
    public class ComponentUtil
    {
        /// <summary>
        /// Transformを初期化する
        /// </summary>
        /// <param name="t">初期化したいTransform</param>
        public static void Normalize(Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localEulerAngles = Vector3.zero;
            t.localScale = Vector3.one;
        }

        /// <summary>
        /// GameObjectを親を起点に生成する
        /// </summary>
        /// <param name="parent">親となるGameObject</param>
        /// <param name="go">生成したいGameObject(外部から持ってきた物)</param>
        /// <returns>生成されたGameObject</returns>
        public static GameObject InstantiateTo(GameObject parent, GameObject go)
        {
            GameObject ins = (GameObject)GameObject.Instantiate(
                go,
                parent.transform.position,
                parent.transform.rotation
            );
            ins.transform.SetParent(parent.transform, false);
            Normalize(ins.transform);
            return ins;
        }

        /// <summary>
        /// GameObjectを生成し、そのGameObjectに張り付いている、Componentクラスを取得する
        /// </summary>
        /// <param name="parent">親となるGameObject</param>
        /// <param name="go">生成したいGameObject(外部から持ってきた物)</param>
        /// <typeparam name="T">Componentクラスの型</typeparam>
        /// <returns>生成されたGameObjectに張り付いてる指定したComponentクラス</returns>
        public static T InstantiateTo<T>(GameObject parent, GameObject go)
            where T : Component
        {
            return InstantiateTo(parent, go).GetComponent<T>();
        }

        /// <summary>
        /// Transform以下に紐付いている子のGameObjectを全て削除する
        /// </summary>
        /// <param name="parent">削除したいTransform</param>
        public static void DeleteAllChildren(Transform parent)
        {
            List<Transform> transformList = new List<Transform>();
            foreach (Transform child in parent)
            {
                transformList.Add(child);
            }
            parent.DetachChildren();
            foreach (Transform child in transformList)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}