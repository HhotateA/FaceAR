using UnityEngine;
using System.Collections;

public static class CaptureScreenAndroid
{
    /// <summary>
    /// スクリーンショットを保存してギャラリーに反映させる
    /// </summary>
    public static void CaptureScreen (MonoBehaviour mb)
    {
        mb.StartCoroutine (CaptureScreenCoroutine ());
    }

    static IEnumerator CaptureScreenCoroutine ()
    {
        // Screenshot を撮る
        string fileName = "screenshot" + System.DateTime.Now.Ticks.ToString () + ".png";
        if (Application.platform == RuntimePlatform.Android) {
            ScreenCapture.CaptureScreenshot ("../../../../DCIM/Camera/" + fileName);
        } else {
            ScreenCapture.CaptureScreenshot (fileName);
        }

        // ファイル保存が終わるまで1フレーム待つ
        yield return new WaitForEndOfFrame();

        // メディアスキャン
        ScanMedia (fileName);
    }

    static void ScanMedia (string fileName)
    {
        if (Application.platform != RuntimePlatform.Android)
            return;
#if UNITY_ANDROID
        using (AndroidJavaClass jcUnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity"))
        using (AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject> ("getApplicationContext"))
        using (AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass ("android.media.MediaScannerConnection"))
        using (AndroidJavaClass jcEnvironment = new AndroidJavaClass ("android.os.Environment"))
        using (AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject> ("getExternalStorageDirectory")) {
            string path = joExDir.Call<string> ("toString") + "/DCIM/Camera/" + fileName;
            Debug.Log ("search path : " + path);
            jcMediaScannerConnection.CallStatic ("scanFile", joContext, new string[] { path }, new string[] { "image/png" }, null);
        }
#endif
    }
}