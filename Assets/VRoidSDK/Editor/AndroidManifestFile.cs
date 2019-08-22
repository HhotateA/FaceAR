using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace VRoidSDK.Editor
{
    public class AndroidManifestFile
    {
        private const string AndroidManifestDirectory = "/Plugins/Android/";
        private const string AndroidManifestName = "AndroidManifest.xml";

        public AndroidManifestFile()
        {
        }

        public bool IsFileExists()
        {
            return File.Exists(ManifestPath);
        }

        public bool CopyFromExample()
        {
            var result = AssetDatabase.CopyAsset("Assets" + AndroidManifestDirectory + AndroidManifestName + ".example",
                                                 "Assets" + AndroidManifestDirectory + AndroidManifestName);
            return result;
        }

        public ManifestXmlDocument MakeManifestXml()
        {
            XmlDocument androidManifestXml = new XmlDocument();
            androidManifestXml.Load(ManifestPath);
            XmlElement root = androidManifestXml.DocumentElement;
            root.SetAttribute("package", PlayerSettings.applicationIdentifier);

            return new ManifestXmlDocument(androidManifestXml);
        }

        public string ManifestPath
        {
            get { return Application.dataPath + AndroidManifestDirectory + AndroidManifestName; }
        }
    }
}