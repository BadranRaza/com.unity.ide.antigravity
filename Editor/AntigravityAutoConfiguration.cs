/*---------------------------------------------------------------------------------------------
 *  Copyright (c) BadranRaza.
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Unity.CodeEditor;
using UnityEditor;
using UnityEngine;

namespace Microsoft.Unity.VisualStudio.Editor
{
	internal static class AntigravityAutoConfiguration
	{
		const string AutoSelectedPathKey = "com.badranraza.ide.antigravity.auto_selected_path";
		const double RetryIntervalSeconds = 1.0;
		const double MaxWaitSeconds = 300.0;
		static double s_StartedAt;
		static double s_NextAttemptAt;

		static readonly ProjectGenerationFlag RecommendedProjectGenerationFlags =
			ProjectGenerationFlag.Embedded |
			ProjectGenerationFlag.Local |
			ProjectGenerationFlag.Registry |
			ProjectGenerationFlag.Git |
			ProjectGenerationFlag.LocalTarBall |
			ProjectGenerationFlag.Unknown;

		internal static void Schedule()
		{
			s_StartedAt = EditorApplication.timeSinceStartup;
			s_NextAttemptAt = 0.0;
			EditorApplication.update -= ConfigureWhenReady;
			EditorApplication.update += ConfigureWhenReady;
		}

		static void ConfigureWhenReady()
		{
			try
			{
				ConfigureWhenReadyUnsafe();
			}
			catch (Exception ex)
			{
				EditorApplication.update -= ConfigureWhenReady;
				Debug.LogWarning($"[Antigravity] Auto-configuration failed: {ex.Message}");
			}
		}

		static void ConfigureWhenReadyUnsafe()
		{
			if (!UnityInstallation.IsMainUnityEditorProcess)
			{
				EditorApplication.update -= ConfigureWhenReady;
				return;
			}

			var now = EditorApplication.timeSinceStartup;
			if (now < s_NextAttemptAt)
				return;

			if (now - s_StartedAt > MaxWaitSeconds)
			{
				EditorApplication.update -= ConfigureWhenReady;
				return;
			}

			if (EditorApplication.isCompiling || EditorApplication.isUpdating)
			{
				s_NextAttemptAt = now + RetryIntervalSeconds;
				return;
			}

			EditorApplication.update -= ConfigureWhenReady;

			var installation = Discovery
				.GetVisualStudioInstallations()
				.FirstOrDefault(candidate => candidate is AntigravityInstallation);

			if (installation == null)
				return;

			var currentIsAntigravity = IsCurrentEditorAntigravity();
			var selectedNow = EnsureAntigravityIsSelected(installation, currentIsAntigravity);

			if (!selectedNow && !currentIsAntigravity)
				return;

			var flagsChanged = EnsureRecommendedProjectGeneration(installation);
			if (selectedNow || flagsChanged || !installation.ProjectGenerator.HasSolutionBeenGenerated())
				installation.ProjectGenerator.Sync();
		}

		static bool IsCurrentEditorAntigravity()
		{
			if (!(CodeEditor.CurrentEditor is VisualStudioEditor))
				return false;

			return AntigravityInstallation.TryDiscoverInstallation(
				CodeEditor.CurrentEditorInstallation,
				out _);
		}

		static bool EnsureAntigravityIsSelected(IVisualStudioInstallation installation, bool currentIsAntigravity)
		{
			if (currentIsAntigravity)
			{
				EditorPrefs.SetString(AutoSelectedPathKey, installation.Path);
				return false;
			}

			var autoSelectedPath = EditorPrefs.GetString(AutoSelectedPathKey, string.Empty);
			if (string.Equals(autoSelectedPath, installation.Path, StringComparison.OrdinalIgnoreCase))
				return false;

			CodeEditor.SetExternalScriptEditor(installation.Path);
			EditorPrefs.SetString(AutoSelectedPathKey, installation.Path);
			Debug.Log($"[Antigravity] Selected {installation.Path} as Unity's external script editor.");
			return true;
		}

		static bool EnsureRecommendedProjectGeneration(IVisualStudioInstallation installation)
		{
			var provider = installation.ProjectGenerator.AssemblyNameProvider;
			var missingFlags = RecommendedProjectGenerationFlags & ~provider.ProjectGenerationFlag;
			if (missingFlags == ProjectGenerationFlag.None)
				return false;

			foreach (ProjectGenerationFlag flag in Enum.GetValues(typeof(ProjectGenerationFlag)))
			{
				if (flag == ProjectGenerationFlag.None)
					continue;

				if (missingFlags.HasFlag(flag))
					provider.ToggleProjectGeneration(flag);
			}

			Debug.Log("[Antigravity] Enabled recommended C# project generation for embedded, local, registry, Git, local tarball and unknown packages.");
			return true;
		}
	}
}
