using System;
using System.Reflection;
using UnityEditor;

namespace KoganeUnityLib
{
	internal static class ConsoleWindowUtils
	{
		public enum ConsoleFlags
		{
			Collapse        = 1 << 0,
			ClearOnPlay     = 1 << 1,
			ErrorPause      = 1 << 2,
			Verbose         = 1 << 3,
			StopForAssert   = 1 << 4,
			StopForError    = 1 << 5,
			Autoscroll      = 1 << 6,
			LogLevelLog     = 1 << 7,
			LogLevelWarning = 1 << 8,
			LogLevelError   = 1 << 9,
			ShowTimestamp   = 1 << 10,
			ClearOnBuild    = 1 << 11,
		}
	}

	internal static class LogEntriesUtils
	{
		private const BindingFlags BINDING_ATTR = BindingFlags.Static | BindingFlags.Public;

		private static readonly Assembly   m_assembly = typeof( AssetDatabase ).Assembly;
		private static readonly Type       m_type     = m_assembly.GetType( "UnityEditor.LogEntries" );
		private static readonly MethodInfo m_info     = m_type.GetMethod( "SetConsoleFlag", BINDING_ATTR );

		public static void SetConsoleFlag( ConsoleWindowUtils.ConsoleFlags bit, bool value )
		{
			m_info.Invoke( null, new object[] { ( int ) bit, value } );
		}
	}

	[InitializeOnLoad]
	internal static class ConsoleWindowFlagsInitializer
	{
		static ConsoleWindowFlagsInitializer()
		{
			LogEntriesUtils.SetConsoleFlag( ConsoleWindowUtils.ConsoleFlags.ClearOnPlay, true );
			LogEntriesUtils.SetConsoleFlag( ConsoleWindowUtils.ConsoleFlags.ShowTimestamp, true );
		}
	}
}