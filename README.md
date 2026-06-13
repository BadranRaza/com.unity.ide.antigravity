# Code Editor Package for Google Antigravity IDE

[![Latest release](https://img.shields.io/github/v/release/BadranRaza/com.unity.ide.antigravity?label=release&sort=semver&color=blue)](https://github.com/BadranRaza/com.unity.ide.antigravity/releases/latest)
[![Unity 2019.4+](https://img.shields.io/badge/Unity-2019.4%2B-black?logo=unity)](https://unity.com/)
[![License: MIT](https://img.shields.io/github/license/BadranRaza/com.unity.ide.antigravity?color=brightgreen)](LICENSE.md)
[![GitHub stars](https://img.shields.io/github/stars/BadranRaza/com.unity.ide.antigravity?style=social)](https://github.com/BadranRaza/com.unity.ide.antigravity/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/BadranRaza/com.unity.ide.antigravity?style=social)](https://github.com/BadranRaza/com.unity.ide.antigravity/network/members)

Use **Google Antigravity IDE** as your Unity script editor, with proper
IntelliSense, project file generation and single-instance window reuse.

Heads up: Google split the Antigravity product line at I/O 2026 (May 19, 2026).
There are now two desktop apps that look similar but do very different things —
the standalone **Antigravity** is an agent-orchestration tool with no code
editor inside it, and **Antigravity IDE** is the VS Code fork you actually
want for writing C#. Older versions of this package (and most of the
community forks I found) matched any `Antigravity*.exe`, which means Unity
ended up opening the agent app every time you double-clicked a script.
v1.0.6+ fixes that — see [Antigravity 2.0 split](#antigravity-20-split) for
the full story.

If this saved you an afternoon of "why is the agent app opening my scripts?",
please ⭐ the repo. That's how other Unity devs find it.

## Features

- **Auto-Discovery**: automatically finds your **Antigravity IDE**
  installation on macOS, Windows and Linux (and ignores the standalone
  Antigravity agent app, which cannot host script editing).
- **Auto-Selection**: selects **Antigravity IDE** as Unity's External Script
  Editor after package import once Unity has finished compiling/importing.
- **IntelliSense**: generates `.sln` and `.csproj` files for complete C#
  IntelliSense and Unity API support inside Antigravity IDE.
- **Package-aware projects by default**: includes embedded, local, registry,
  Git, local tarball and unknown packages in generated `.csproj` files so
  package asmdefs are visible to code intelligence.
- **Workspace Setup**: writes `.vscode/` workspace config files
  (`launch.json`, `settings.json`, `extensions.json`) that Antigravity IDE
  reads as a VS Code fork.
- **Reuse Window**: optional setting to open files in an already-running
  Antigravity IDE window instead of launching a new one. Backed by a scan of
  Antigravity IDE's own `workspaceStorage` directory.
- **Insider builds supported**: `Antigravity IDE - Insider` /
  `antigravity-ide-insiders` are discovered on all three platforms.

## Installation

Install via Unity Package Manager using the git URL:

1. Open `Window > Package Manager`
2. Click `+` → **Add package from git URL...**
3. Enter:
   ```
   https://github.com/BadranRaza/com.unity.ide.antigravity.git
   ```
4. Click **Add**

The package will appear in Package Manager under the name
**`com.badranraza.ide.antigravity`** and can be updated from there when new
versions are released.

After Unity finishes importing and compiling, the package automatically
selects **Antigravity IDE** as the External Script Editor when a valid
Antigravity IDE install is discovered. It also enables the recommended
`.csproj` generation sources for Unity packages.

> **Alternative**: if you cloned the repo locally, use
> `+ → Add package from disk...` and point to the `package.json`.

> **Upgrading from v1.x?** v2.0.0 renamed the UPM `name` field from
> `com.unity.ide.antigravity` to `com.badranraza.ide.antigravity` (the old
> name used Unity's reserved `com.unity.*` namespace and was rejected by
> OpenUPM). To upgrade: in Package Manager, remove the old
> `com.unity.ide.antigravity` entry and re-add via the same git URL above.
> Your External Tools selection survives the rename. The GitHub repo URL
> is unchanged.

## Configuration

1. Install **Antigravity IDE** from
   [antigravity.google](https://antigravity.google).
2. Install this package through Unity Package Manager.
3. Wait for Unity to finish importing/compiling. The package automatically
   selects **Antigravity IDE** from the **External Script Editor** dropdown.
   If a plain "Antigravity" entry was previously selected, that was the agent
   app and this package will replace it with the actual IDE when discovered.
4. The package enables `.csproj` generation for embedded, local, registry,
   Git, local tarball and unknown packages. You can still adjust these later
   from `Unity > Preferences > External Tools` (macOS) or
   `Edit > Preferences > External Tools` (Windows / Linux).

### Reuse Existing Window

When Antigravity IDE is selected as the editor, a
**"Reuse existing Antigravity window"** toggle appears in Preferences. When
enabled, double-clicking a script in Unity will open it in an already-running
Antigravity IDE instance instead of launching a new one.

## Antigravity 2.0 split

At Google I/O 2026 (May 19, 2026) Google split the Antigravity product line
into two separate desktop applications:

| Product | What it is | Default install dir (Windows) | Process / exe name |
|---|---|---|---|
| **Antigravity** (2.0) | Agent-first standalone desktop app. Multi-agent orchestration UI. **Not a code editor.** | `…\Programs\Antigravity\Antigravity.exe` | `Antigravity.exe` |
| **Antigravity IDE** | VS Code fork. The actual code editor. **This is what Unity needs.** | `…\Programs\Antigravity IDE\Antigravity IDE.exe` | `Antigravity IDE.exe` |

Both share the same Code-fork shell. They are told apart by
`resources/app/package.json` `name` field (`"Antigravity"` vs
`"Antigravity IDE"`), which this package verifies during discovery. Older
versions of this package (≤ 1.0.5) matched any `Antigravity*.exe`, which on
machines with both products installed could route Unity script opens into the
agent app. Starting in **v1.0.6**, only Antigravity IDE installs are listed.

## How It Works

- **Workspace config** is written to `.vscode/` in your Unity project root —
  Antigravity IDE reads these as a VS Code fork.
- **Extensions** are looked up from `~/.antigravity-ide/extensions/`
  (`~/.antigravity-ide-insiders/extensions/` for insider builds) — Antigravity
  IDE's user-level extension directory.
- **Window reuse** works by reading Antigravity IDE's `workspaceStorage`
  directory to find which workspace a running instance has open:
  - Windows: `%APPDATA%\Antigravity IDE\User\workspaceStorage`
  - macOS: `~/Library/Application Support/Antigravity IDE/User/workspaceStorage`
  - Linux: `~/.config/Antigravity IDE/User/workspaceStorage`

## Requirements

- Unity 2019.4 or later
- **Antigravity IDE** installed from
  [antigravity.google](https://antigravity.google) (the standalone
  Antigravity 2.0 agent app does not work as a Unity script editor)

## Troubleshooting / FAQ

### Unity keeps opening the Antigravity 2.0 agent app instead of Antigravity IDE

This was the whole reason v1.0.6 exists. Update the package to v1.0.7 or
later, then in `Edit > Preferences > External Tools` re-select **Antigravity
IDE** from the dropdown (your old saved preference might still point at the
agent app's executable; re-picking fixes it).

If you've also got the standalone Antigravity 2.0 agent app installed and
want to make absolutely sure it can't be picked: the detection now reads
`resources/app/package.json` inside each install and rejects anything whose
product name isn't exactly `Antigravity IDE`. You don't have to uninstall
the agent app for the IDE to work.

### "Antigravity IDE" doesn't appear in the External Script Editor dropdown

Three things to check:

1. You've actually installed **Antigravity IDE**, not just the agent app.
   Grab the IDE installer from [antigravity.google](https://antigravity.google).
   On Windows it lands in `%LOCALAPPDATA%\Programs\Antigravity IDE\Antigravity IDE.exe`
   by default, on macOS in `/Applications/Antigravity IDE.app`, on Linux at
   `/usr/bin/antigravity-ide` (or the XDG `.desktop` entry).
2. The package actually imported. Open `Window > Package Manager`, switch the
   filter to "In Project", confirm **Antigravity IDE Editor** is listed at
   v1.0.7 or newer.
3. Unity needs a domain reload after installing the package. Closing and
   reopening Unity is the surefire way; toggling Play mode usually works too.

If it still doesn't show up, click **Browse** in External Tools and point
manually at `Antigravity IDE.exe` (Windows), `Antigravity IDE.app` (macOS) or
the `antigravity-ide` binary (Linux). The discovery code will accept it as
long as the manifest's product name is `Antigravity IDE`.

### IntelliSense isn't working inside Antigravity IDE

Antigravity IDE is a VS Code fork, so the same constraints apply: Microsoft's
official C# / C# Dev Kit / Unity extensions are licensed only for Visual
Studio Code itself, not for forks. The community-recommended replacement is
**DotRush** (open-source Roslyn-based C# language server) — install it from
the Extensions panel inside Antigravity IDE, then make sure the `.sln`
generated by this package is at the workspace root.

The recommendations file this package writes (`.vscode/extensions.json`)
suggests `visualstudiotoolsforunity.vstuc`, which is the standard pointer
used by the upstream `com.unity.ide.vscode` package this is forked from. On
Antigravity IDE specifically, swap to DotRush.

### Reuse-existing-window does nothing

The toggle reads Antigravity IDE's own `workspaceStorage` directory
(`%APPDATA%\Antigravity IDE\User\workspaceStorage` on Windows, the equivalent
on macOS / Linux) and matches each running `Antigravity IDE` process by the
workspace path stored there. If you've never opened the Unity project in
Antigravity IDE before, there's nothing to match — open it once normally,
then the toggle starts working.

If you previously used the older Antigravity 1.x and have data in the
legacy `\Roaming\Antigravity\User\workspaceStorage`, v1.0.6+ no longer reads
it (that path now belongs to the agent app). Either re-open your project
once in Antigravity IDE, or copy the relevant `workspaceStorage` subfolder
across manually.

### Insider build of Antigravity IDE isn't detected

Insider builds (`Antigravity IDE - Insider.exe`, `antigravity-ide-insiders`,
`Antigravity IDE - Insider*.app`) are detected starting in v1.0.7. If you're
on the insider channel and it doesn't show up, make sure the package is at
v1.0.7 or newer, then re-open Unity to force a domain reload.

## How this fork compares to the other community packages

There are several community packages that all try to do the same thing.
Here's where they sit as of May 2026, in case you're choosing:

| Fork | Last push | Stars | Antigravity 2.0 fix? |
|---|---|---|---|
| **BadranRaza/com.unity.ide.antigravity** (this one) | 2026-05-23 | live | ✅ v1.0.6+ — full detection + manifest check |
| alexakajustin/Antigravity-Unity | 2026-05-20 | 1 | ⚠️ partial — README rename only, runtime still hits `/Antigravity.exe` |
| billythekidz/UnityAntigravityIDE | 2026-03-21 | 15 | ❌ predates the split |
| usmanbutt-dev/antigravity-unity | 2026-02-09 | 21 | ❌ predates the split |
| akshwpsh/com.unity.ide.antigravity | 2026-02-25 | 3 | ❌ predates the split |
| TermWay/unity-ide-antigravity | 2025-11-30 | 21 | ❌ predates the split |
| kientux/com.unity.ide.antigravity | 2025-12-12 | 0 | ❌ predates the split |

If you're already on one of those and it's working for you, you don't have
to switch. If Unity is opening the agent app on every script double-click,
this is what fixed it for me.

## Support the project

If this package made your Unity + Antigravity IDE setup work:

- ⭐ **Star the repository** — biggest single thing you can do to help
  others find it.
- 🔁 Share the repo link with other Unity devs who use Antigravity IDE.
- 💬 Open an issue or discussion if you hit a bug or have a feature request.
- 🔧 Pull requests welcome — see open issues for ideas.

## License

[MIT](LICENSE.md). Free to use in personal, commercial and Asset Store
projects. Includes upstream MIT-licensed code from Unity Technologies and
Microsoft Corporation (the `com.unity.ide.vscode` package this is forked
from).

## Maintainer

Maintained by **Badran Raza** ([@BadranRaza](https://github.com/BadranRaza)) —
Unity / game-side software developer based in Lahore, Pakistan. If you've
hit a bug or want to contribute, the [Issues](https://github.com/BadranRaza/com.unity.ide.antigravity/issues)
and [Discussions](https://github.com/BadranRaza/com.unity.ide.antigravity/discussions)
tabs are the right entry points. For the story of why this package exists
and how the Antigravity IDE detection works under the hood, see
[`Documentation~/antigravity-2-0-fix.md`](Documentation~/antigravity-2-0-fix.md).
