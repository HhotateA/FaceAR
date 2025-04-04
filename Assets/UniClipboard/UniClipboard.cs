﻿using UnityEngine;
using System.Collections;

namespace UniClipboard
{
	public static class Clipboard
	{
		public static IClipboard Current { get; set; } = Create();

		public static IClipboard Create()
		{
//#if UNITY_EDITOR || UNITY_STANDALONE
//			return new Clipboards.Standalone();
#if UNITY_IOS
			return new Clipboards.iOS();
#else
			return new Clipboards.Standalone();
#endif
		}

		public static string Text
		{
			set { Current.SetText(value); }
			get { return Current.GetText(); }
		}
	}

	public interface IClipboard
	{
		string GetText();
		void SetText(string text);
	}
}
