using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using WMPLib;
using System.IO;
using System.Reflection;

namespace Jeopardy
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2
	{
		private WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
		private BuildEvents buildBegin;
		private BuildEvents buildDone;

		/// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
			buildBegin = _applicationObject.Events.BuildEvents;
			buildDone = _applicationObject.Events.BuildEvents;
			System.Diagnostics.Debug.WriteLine("Connected!");
		}

		void buildBegin_OnBuildBegin(vsBuildScope Scope, vsBuildAction Action)
		{
			System.Diagnostics.Debug.WriteLine("Build Begin");
			playSong();
		}

		void buildDone_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
		{
			System.Diagnostics.Debug.WriteLine("Build End");
			player.controls.stop();
		}

		private void playSong()
		{
			//WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();
			player.URL = @"C:\development\Interns\Jeopardy\Jeopardy Theme.mp3";
			//player.URL = @"c:\users\acloudy\documents\visual studio 2010\Projects\Jeopardy\Jeopardy\Jeopardy Theme.mp3";
			player.settings.setMode("loop", true);
			player.controls.play();
			System.Diagnostics.Debug.WriteLine("Play Music");
		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
			buildBegin.OnBuildBegin += new _dispBuildEvents_OnBuildBeginEventHandler(buildBegin_OnBuildBegin);
			buildDone.OnBuildDone += new _dispBuildEvents_OnBuildDoneEventHandler(buildDone_OnBuildDone);
		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
			buildBegin.OnBuildBegin -= new _dispBuildEvents_OnBuildBeginEventHandler(buildBegin_OnBuildBegin);
			buildDone.OnBuildDone -= new _dispBuildEvents_OnBuildDoneEventHandler(buildDone_OnBuildDone);
		}
		
		private DTE2 _applicationObject;
		private AddIn _addInInstance;
	}
}