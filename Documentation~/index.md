# Code Editor Package for Google Antigravity IDE

## About

The **Antigravity Editor** package integrates **Google Antigravity IDE**
(the VS Code-based code editor in the Antigravity product line) as the
external code editor for Unity. It provides:

- Automatic discovery of your Antigravity IDE installation (and rejects the
  standalone Antigravity 2.0 agent app, which is a separate, non-editor
  product)
- Automatic selection of Antigravity IDE as Unity's External Script Editor
  after package import, once Unity has finished compiling/importing
- `.sln` and `.csproj` generation for full C# IntelliSense
- Recommended `.csproj` generation for embedded, local, registry, Git, local
  tarball and unknown packages by default
- Workspace config setup (`.vscode/launch.json`, `settings.json`, `extensions.json`)
- Optional single-instance window reuse, backed by a scan of Antigravity
  IDE's own `workspaceStorage`

## Installation

1. Open `Window > Package Manager` in Unity
2. Click `+` → **Add package from git URL...**
3. Enter `https://github.com/BadranRaza/com.unity.ide.antigravity.git`
4. Click **Add**

## Requirements

- Unity 2019.4 or later
- [Antigravity IDE](https://antigravity.google) installed — note that the
  standalone "Antigravity" agent app (Antigravity 2.0, launched at Google
  I/O 2026) is a different product and cannot serve as a Unity script editor

## License

MIT. See the package's `LICENSE.md` for full terms.

## Submitting Issues

Please open issues on the GitHub repository for this package.
