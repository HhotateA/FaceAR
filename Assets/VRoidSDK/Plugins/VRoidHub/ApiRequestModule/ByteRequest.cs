using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace VRoidSDK
{
    /// <summary>
    /// VRoid Hub のAPIをリクエストしてバイト配列で受け取るためのクラス
    /// </summary>
    /// <typeparam name="T">リクエスト結果の型</typeparam>
    public class ByteRequest : ApiRequestBase<byte[]>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="requestPath">リクエストするAPIのURL</param>
        public ByteRequest(string requestPath) : base(requestPath)
        {
            ResponseConverter = new ByteResponseConverter();
        }
    }
}