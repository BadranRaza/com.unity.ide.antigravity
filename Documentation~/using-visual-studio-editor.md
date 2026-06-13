# Using the Antigravity Editor Package

## Set Antigravity IDE as External Script Editor

1. Install **Antigravity IDE** from
   [antigravity.google](https://antigravity.google). This is the VS Code-fork
   code editor — **not** the standalone "Antigravity" agent app (Antigravity
   2.0), which cannot host script editing.
2. Install this package through Unity Package Manager.
3. Wait for Unity to finish importing/compiling. The package automatically
   selects **Antigravity IDE** as Unity's External Script Editor when a valid
   install is discovered.
4. Open **Unity > Preferences > External Tools** (macOS) or
   **Edit > Preferences > External Tools** (Windows / Linux) only if you want
   to inspect or customize the generated project settings.

## Generate .csproj Files

The package generates `.csproj` and `.sln` files so Antigravity IDE has full
C# IntelliSense and Unity API awareness. You can use the checkboxes to
customize which package types get a `.csproj` file:

| Setting               | Description                                                          |
| --------------------- | -------------------------------------------------------------------- |
| **Embedded packages** | Packages inside your project's `Packages/` folder                    |
| **Local packages**    | Packages installed from a local path outside the project             |
| **Registry packages** | Packages from Unity or a custom package registry                     |
| **Git packages**      | Packages installed via a Git URL                                     |
| **Built-in packages** | Packages bundled with the Unity installation                         |
| **Tarball packages**  | Packages installed from a local `.tgz` archive                       |
| **Unknown packages**  | Packages with an unrecognized or missing origin                      |
| **Player projects**   | Generates an extra `ProjectName.Player.csproj` for player assemblies |

Embedded, local, registry, Git, local tarball and unknown packages are enabled
automatically by default. Click **Regenerate project files** only when you
manually change these settings and want to apply them immediately.

## Workspace Config Files

When opening a project, the package automatically creates or patches these
files inside `.vscode/` in your Unity project root (Antigravity IDE reads
these as a VS Code-based editor):

| File                      | Purpose                                                                                     |
| ------------------------- | ------------------------------------------------------------------------------------------- |
| `.vscode/extensions.json` | Recommends the `visualstudiotoolsforunity.vstuc` Unity extension                            |
| `.vscode/settings.json`   | Excludes Unity binary/generated files from the file explorer; sets `dotnet.defaultSolution` |
| `.vscode/launch.json`     | Adds an "Attach to Unity" debug configuration                                               |

To prevent the package from patching an existing file, create a
`.vstupatchdisable` file inside `.vscode/`.

## Reuse Existing Window

When Antigravity IDE is the active editor, a
**"Reuse existing Antigravity window"** toggle appears in Preferences. When
enabled, double-clicking a script opens it in the already-running Antigravity
IDE instance that has the project open, instead of launching a new window.

Process matching and workspace storage scanning are both restricted to the
**Antigravity IDE** product (process name `Antigravity IDE`, storage path
`Antigravity IDE/User/workspaceStorage`). The standalone Antigravity agent
app is never matched, even if it is also running.
