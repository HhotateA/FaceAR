# VRoid SDK
VRoid SDKのご利用ありがとうございます。  
本ライブラリのご利用にあたって、まず以下のガイドラインを必ずご一読ください。  
よろしくお願いいたします。

## SDK利用のガイドライン
### 3Dモデル作者の意思を尊重した運用を行ってください
- 利用条件表示やクレジット表示など、VRMの要件は守ってください。
- 暴力表現を使うのに利用条件で明示しないなど、嘘の情報を登録しないでください。
### SDKをリバースエンジニアリングしないでください
- バイナリを改造して、3DモデルファイルをSDKが意図しない方法で利用しないでください。
- Web APIを直接操作するサーバーサイドアプリを作らないでください。
### 開発者登録していない人へSDKを二次配布しないでください
- SDKのバイナリファイルや配布用URLを他社や友人へ許可なく渡さないでください。
- 他人が利用することを意図してアプリケーション追加を行わないでください。


## VRoid SDKのインストール
ダウンロードしたVRoidSDK.unitypackageファイルを

Assets > Import Package > Custom Package...

からインポートすることでインストールが完了します

## SDK設定
- [アプリケーション管理ページ](https://hub.vroid.com/oauth/applications)からOAuth連携アプリケーションを作成し、Application UIDとSecretを取得する
- `Assets/VRoidSDK/SDKConfigurations/SDKConfiguration.assets`を開き、Application Id/Secretをそれぞれ設定する
- モバイルアプリケーションの場合は、管理ページで設定したリダイレクトURIをAndroid/IOS Url Schemeにそれぞれ設定する
    - ここで設定したURLスキーマはビルド前、あるいはビルド後にプロジェクトに埋め込まれます
    - Androidの場合はアプリケーションビルド前に、`Assets/Plugins/Android/AndroidManifest.xml`にUrlスキーマに変更があれば追加されます。もし、AndroidManifestが存在しない場合`AndroidManifest.xml.example`からコピーされて追加されます。詳しくは、`Assets/VRoidSDK/Editor/AndroidManifestAddUrlScheme.cs`を参照してください　
    - iOSの場合はビルド完了後に、出力される`Info.plist`に埋め込まれます。詳しくは、`Assets/VRoidSDK/Editor/UrlSchemePostprocessor.cs`を参照ください

## Getting Started
### ExampleのPrefabを使う方法
`Assets/VRoidSDK/Examples/Prefabs/VRoidHubController`を使えば、独自にUIを作らなくてもExample相当のUIまで実現することが可能です

- まず、UIを表示したいCanvasと、キャラクターを表示するEmptyオブジェクト`CharactersRoot`、`VRoidHubController`PrefabをUnityのシーンに配置します
- `CharacterRoot`に`/Assets/VRoidSDK/Examples/Scripts/MainSystem.cs`スクリプトをアタッチします
- `MainSystem.cs`のControllerに追加した`VRoidHubController`を、Viewに画面を表示するCanvasをそれぞれ設定します
- あとは、別途ボタンなどから`VRoidHubController.Open`を実行するだけで、キャンバスの表示、`VRoidHubController.Close`を実行することでキャンバスの非表示などが簡単に行えるようになります
- VRMが読み込まれた時の処理は、`MainSystem#Start`で定義されているので、ここを修正することで自由に変更することができます

### Prefabを使わない方法
[こちら](https://developer.vroid.com)を参照してください

## API Document
[こちら](https://developer.vroid.com/sdk/docs/VRoidSDK.html)を参照してください

## サポートランタイム設定
| Scripting Runtime Version | Scripting Backend※| Api Compatibility Level |
| ------------------------- | ----------------- | ----------------------- |
|    .NET 3.5 Equivalent    |       Mono        |      .NET 2.0 Subset    |
|    .NET 4.x Equivalent    |       Mono        |         .NET4.x         |

※ Android端末の場合、Scripting BackendにMonoを採用することはできません。IL2CPPをご利用ください

## パッケージビルドバージョン
Unity 2018.2.17f1
