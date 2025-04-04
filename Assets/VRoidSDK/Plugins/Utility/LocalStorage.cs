﻿using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace VRoidSDK
{
    /// <summary>
    /// SDKのデータを保存しておく領域
    /// </summary>
    public class LocalStorage
    {
        private static HashSet<string> volatilityData = new HashSet<string>();
        private static Dictionary<string, object> savedData;

        private static Dictionary<string, object> SavedData
        {
            set { savedData = value; }
            get
            {
                if (savedData == null)
                {
                    savedData = Load();
                }
                return savedData;
            }
        }

        /// <summary>
        /// <para>コンストラクタ</para>
        /// <para>ローカルストレージに保存されているデータをメモリにのせる</para>
        /// </summary>
        static LocalStorage()
        {
            // データへのアクセスを高速にするためにあらかじめロードしておく
            SavedData = Load();
        }

        /// <summary>
        /// <para>key-value形式でデータをメモリにのせる</para>
        /// <para>※データを保存して、データを永続化はまだ行われていない</para>
        /// </summary>
        /// <param name="key">データを参照するキー</param>
        /// <param name="value">保存するデータ</param>
        public static void SetValue(string key, object value)
        {
            volatilityData.Remove(key);
            if (SavedData.ContainsKey(key))
            {
                SavedData[key] = value;
            }
            else
            {
                SavedData.Add(key, value);
            }
        }

        /// <summary>
        /// <para>key-value形式でデータをメモリにのせる</para>
        /// <para>※このメソッドでメモリに乗せたデータはSave保存してもストレージには保存されない。(アプリを落としたら消える)</para>
        /// </summary>
        /// <param name="key">データを参照するキー</param>
        /// <param name="value">保存するデータ</param>
        public static void SetVolatilityValue(string key, object value)
        {
            SetValue(key, value);
            volatilityData.Add(key);
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータに指定したkeyが存在するかどうか判別する</para>
        /// </summary>
        /// <param name="key">確認するキー</param>
        /// <returns>データが存在するか</returns>
        public static bool HasKey(string key)
        {
            return SavedData.ContainsKey(key);
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータを指定したObjectの形に変換して取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>変換されたオブジェクト</returns>
        /// <typeparam name="T">取り出すオブジェクトの型</typeparam>
        public static T GetGenericObject<T>(string key, T defaultValue = default(T))
        {
            if (SavedData.ContainsKey(key))
            {
                if (SavedData[key] is T)
                {
                    return (T)SavedData[key];
                }
                return JsonConvert.DeserializeObject<T>(GetString(key));
            }
            return defaultValue;
        }

        /// <summary>
        /// 特定の型の配列で取得する。型がマッチしなかった場合には要素はフィルタリングされる。
        /// </summary>
        /// <returns>Tの配列</returns>
        /// <typeparam name="T">戻り値の配列の型</typeparam>
        public static T[] GetGenericObjectArray<T>()
        {
            List<T> list = new List<T>();

            foreach (var key in SavedData.Keys)
            {

                try
                {
                    T value = GetGenericObject<T>(key);
                    list.Add(value);
                } catch (Newtonsoft.Json.JsonReaderException)
                {
                    // 変換できない場合、無視する
                }
            }
            return list.ToArray();
        }


        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをfloat形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>floatに変換されたオブジェクト</returns>
        public static float GetFloat(string key, float defaultValue = 0.0F)
        {
            if (SavedData.ContainsKey(key))
            {
                return Convert.ToSingle(SavedData[key]);
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをdouble形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>doubleに変換されたオブジェクト</returns>
        public static double GetDouble(string key, double defaultValue = 0.0d)
        {
            if (SavedData.ContainsKey(key))
            {
                return Convert.ToDouble(SavedData[key]);
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをint形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>intに変換されたオブジェクト</returns>
        public static int GetInt(string key, int defaultValue = 0)
        {
            if (SavedData.ContainsKey(key))
            {
                return Convert.ToInt32(SavedData[key]);
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをuint形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>long intに変換されたオブジェクト</returns>
        public static long GetLong(string key, long defaultValue = 0)
        {
            if (SavedData.ContainsKey(key))
            {
                return Convert.ToInt64(SavedData[key]);
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをstring形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>文字列に変換されたオブジェクト</returns>
        public static string GetString(string key, string defaultValue = "")
        {
            if (SavedData.ContainsKey(key))
            {
                return SavedData[key].ToString();
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをDateTime形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>日付に変換されたオブジェクト</returns>
        public static DateTime GetDateTime(string key, DateTime defaultValue = new DateTime())
        {
            if (SavedData.ContainsKey(key))
            {
                return (DateTime)SavedData[key];
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>key-value形式でメモリにのっているデータをBoolean形式で取得する</para>
        /// </summary>
        /// <param name="key">参照するキー</param>
        /// <param name="defaultValue">存在しないときのデフォルト値</param>
        /// <returns>booleanに変換されたオブジェクト</returns>
        public static bool GetBoolean(string key, bool defaultValue = false)
        {
            if (SavedData.ContainsKey(key))
            {
                return Convert.ToBoolean(SavedData[key]);
            }
            return defaultValue;
        }

        /// <summary>
        /// <para>指定したKeyをメモリから削除する</para>
        /// </summary>
        /// <param name="key">削除するデータのキー</param>
        public static void DeleteKey(string key)
        {
            if (SavedData.ContainsKey(key))
            {
                SavedData.Remove(key);
            }
        }

        /// <summary>
        /// <para>今メモリに乗っているものを捨てて、ディスクからデータを読み込む</para>
        /// </summary>
        public static void Reload()
        {
            SavedData = Load();
        }

        /// <summary>
        /// <para>すべてのデータをメモリから削除する</para>
        /// </summary>
        public static void Clear()
        {
            SavedData.Clear();
        }

        /// <summary>
        /// <para>メモリにのっているデータをローカルストレージに保存する</para>
        /// </summary>
        public static void Save()
        {
            Dictionary<string, object> willSaveData = new Dictionary<string, object>();
            List<string> keys = SavedData.Keys.ToList();
            for (int i = 0; i < keys.Count; ++i)
            {
                if (volatilityData.Contains(keys[i]))
                {
                    continue;
                }
                willSaveData.Add(keys[i], SavedData[keys[i]]);
            }

            string json = JsonConvert.SerializeObject(willSaveData);
            EncriptionLocalStorageFile.WriteJson(json);
        }

        /// <summary>
        /// <para>ローカルストレージに保存されているデータを読み込む</para>
        /// </summary>
        /// <returns>ローカルストレージ中のデータ一覧</returns>
        public static Dictionary<string, object> Load()
        {
            string json = EncriptionLocalStorageFile.ReadJson();
            if (string.IsNullOrEmpty(json))
            {
                return new Dictionary<string, object>();
            }else{
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
        }
    }
}