using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace VRoidSDK
{
    /// <summary>
    /// VRoid Hub のAPIをリクエストするためのクラス
    /// </summary>
    /// <typeparam name="T">リクエスト結果の型</typeparam>
    public class ApiRequestBase<T>
    {
        /// <summary>
        /// APIのリクエストパス
        /// </summary>
        /// <returns>`/api`から始まるリクエストパス</returns>
        public readonly string RequestPath;

        /// <summary>
        /// リクエストに使うメソッド
        /// </summary>
        /// <returns>リクエストメソッド (デフォルト: HTTPMethods.Get)</returns>
        public HTTPMethods Methods = HTTPMethods.Get;

        /// <summary>
        /// リクエストのヘッダ情報
        /// </summary>
        /// <returns>リクエストヘッダ (デフォルト: null)</returns>
        public Dictionary<string, string> Headers = null;

        /// <summary>
        /// リクエストのパラメータ
        /// </summary>
        /// <returns>リクエストパラメータ (デフォルト: null)</returns>
        public WWWForm Params = null;

        /// <summary>
        /// WebResponseを加工するコンバーター
        /// </summary>
        /// <returns>コンバーター</returns>
        protected ResponseConverterBase<T> ResponseConverter;

        /// <summary>
        /// 認証機能を持ったモジュール
        /// </summary>
        /// <returns>認証機能を持ったモジュール</returns>
        protected IAuthentication Authenticate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="requestPath">リクエストするAPIのURL</param>
        public ApiRequestBase(string requestPath)
        {
            RequestPath = requestPath;
            Authenticate = Authentication.Instance;
        }

        /// <summary>
        /// Apiへのリクエストを実行する
        /// </summary>
        /// <remarks>
        /// アクセストークンが切れていた場合は自動でリフレッシュして再度リクエストする
        /// </remarks>
        /// <param name="onSuccess">APIへのリクエストに成功した時のコールバック</param>
        /// <param name="onProgress">APIリクエスト中のコールバック</param>
        /// <param name="onError">エラー発生時のコールバック</param>
        /// <typeparam name="T">成功した時の戻り値の型</typeparam>
        public void SendRequest(Action<T, ApiLinksFormat> onSuccess, Action<float> onProgress, Action<ApiErrorFormat> onError)
        {
            RequestCommon(
                onSuccess: (response) => ResponseConverter.Convert(
                    response,
                    (responseTemplate) => onSuccess(responseTemplate.data, responseTemplate._links),
                    onError
                ),
                onProgress: onProgress,
                onError: onError
            );
        }

        /// <summary>
        /// Apiへのリクエストを実行する
        /// </summary>
        /// <remarks>
        /// アクセストークンが切れていた場合は自動でリフレッシュして再度リクエストする
        /// </remarks>
        /// <param name="onSuccess">APIへのリクエストに成功した時のコールバック</param>
        /// <param name="onProgress">APIリクエスト中のコールバック</param>
        /// <param name="onError">エラー発生時のコールバック</param>
        /// <typeparam name="T">成功した時の戻り値の型</typeparam>
        public void SendRequest(Action<T> onSuccess, Action<float> onProgress, Action<ApiErrorFormat> onError)
        {
            SendRequest(
                onSuccess: (data, links) => onSuccess(data),
                onProgress: onProgress,
                onError: onError
            );
        }

        protected void RequestCommon(Action<IWebResponse> onSuccess, Action<float> onProgress, Action<ApiErrorFormat> onError)
        {
            AuthorizedRequest(
                onSuccess: onSuccess,
                onProgress: onProgress,
                onError: (response) =>
                {
                    if (response.StatusCode == 401)
                    {
                        RefreshAndRetry(onSuccess, onError);
                    }
                    else
                    {
                        convertErrorResponse(response, onError);
                    }
                }
            );
        }

        protected void AuthorizedRequest(Action<IWebResponse> onSuccess, Action<float> onProgress, Action<IWebResponse> onError)
        {
            Authenticate.AuthorizedRequest(
                requestPath: RequestPath,
                param: Params,
                methods: Methods,
                headers: Headers,
                onSuccess: onSuccess,
                onProgress: onProgress,
                onError: onError
            );
        }

        protected void convertErrorResponse(IWebResponse response, Action<ApiErrorFormat> onError)
        {
            onError(ResponseConverter.ConvertError(response));
        }

        protected void RefreshAndRetry(Action<IWebResponse> onSuccess, Action<ApiErrorFormat> onError)
        {
            Authenticate.RefreshExistAccountForce(
                (bool isAuthSuccess) =>
                {
                    if (isAuthSuccess)
                    {
                        AuthorizedRequest(
                            onSuccess: onSuccess,
                            onProgress: null,
                            onError: (response) => convertErrorResponse(response, onError)
                        );
                    }
                    else
                    {
                        if (onError != null)
                        {
                            onError(new ApiErrorFormat()
                            {
                                code = "AUTHORIZED_ERROR",
                                message = "Authorize Request Fail"
                            });
                        }
                    }
                },
                (Exception e) =>
                {
                    if (onError != null)
                    {
                        onError(new ApiErrorFormat()
                        {
                            code = "UNKNOWN_ERROR",
                            message = e.Message
                        });
                    }
                }
            );
        }
    }
}