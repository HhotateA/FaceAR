using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TestCapture : MonoBehaviour
{
    [SerializeField] private GameObject[] uis;
    private string _fileName = "";
    public bool screenShot;
    void Start(){
        screenShot = false;
    }
    public void ScreenShot(){
        StartCoroutine(ScreenShotEmu());
    }
    IEnumerator ScreenShotEmu(){
        screenShot = true;
        for(int i=0;i<uis.Length;i++) uis[i].SetActive(false);
        _fileName = "Screenshot" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        yield return CaptureScreenshotProcess();
        for(int i=0;i<uis.Length;i++) uis[i].SetActive(true); 
        screenShot = false;
        yield return MediaDirWriteFileProcess();
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/" + _fileName);
        //CaptureScreenAndroid.CaptureScreen (this);
    }

    private IEnumerator WriteFileProcess()
    {
        _fileName = "Screenshot" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        yield return CaptureScreenshotProcess();
        yield return MediaDirWriteFileProcess();
    }

    private IEnumerator CaptureScreenshotProcess()
    {
        Debug.Log( "CaptureScreenshotProcess" );
        yield return new WaitForEndOfFrame();
        string path = null;
        path = Application.persistentDataPath + "/" + _fileName;

        Debug.Log( "BeginCaptureScreenshot:" + path );
        ScreenCapture.CaptureScreenshot( _fileName );
        Debug.Log( "AfterCaptureScreenshot:" + path );

        while ( File.Exists( path ) == false )
        {
            Debug.Log( "NoFile:" + path );
            yield return new WaitForEndOfFrame();
        }

        Debug.Log( "CaptureOK:" + path );
        scanFile( Application.persistentDataPath, null );//"image/png";
    }

    private IEnumerator MediaDirWriteFileProcess()
    {
        var path = Application.persistentDataPath + "/" + _fileName;
        while ( File.Exists( path ) == false )
        {
            Debug.Log( "NoFile:" + path );
            yield return new WaitForEndOfFrame();
        }
        // 保存パスを取得
        using( AndroidJavaClass jcEnvironment = new AndroidJavaClass("android.os.Environment") )
        using(AndroidJavaObject joPublicDir = jcEnvironment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", jcEnvironment.GetStatic<string>( "DIRECTORY_PICTURES"/*"DIRECTORY_DCIM"*/ ) ) )
        {
            var outputPath = joPublicDir.Call<string>( "toString" );
            Debug.Log( "MediaDir:" + outputPath );
//              outputPath += "/100ANDRO/" + _fileName;
            outputPath += "/Screenshots/" + _fileName;
            var pngBytes = File.ReadAllBytes( path );
            File.WriteAllBytes( outputPath, pngBytes );
            yield return new WaitForEndOfFrame();
            while ( File.Exists( outputPath ) == false )
            {
                Debug.Log( "NoFile:" + outputPath );
                yield return new WaitForEndOfFrame();
            }
            Debug.Log( "MediaDirWriteFileOK:" + outputPath );
            scanFile( outputPath, null );
        }
    }

    static void scanFile( string path, string mimeType )
    {
        using( AndroidJavaClass jcUnityPlayer = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
        using( AndroidJavaObject joActivity = jcUnityPlayer.GetStatic<AndroidJavaObject>( "currentActivity" ) )
        using( AndroidJavaObject joContext = joActivity.Call<AndroidJavaObject>( "getApplicationContext" ) )
        using( AndroidJavaClass jcMediaScannerConnection = new AndroidJavaClass( "android.media.MediaScannerConnection" ) )
        using( AndroidJavaClass jcEnvironment = new AndroidJavaClass( "android.os.Environment" ) )
        using( AndroidJavaObject joExDir = jcEnvironment.CallStatic<AndroidJavaObject>( "getExternalStorageDirectory" ) )
        {
            Debug.Log( "scanFile:" + path );
            var mimeTypes = ( mimeType != null ) ? new string[] { mimeType } : null;
            jcMediaScannerConnection.CallStatic( "scanFile", joContext, new string[] { path }, mimeTypes, null );
        }
        Handheld.StopActivityIndicator();
    }
}