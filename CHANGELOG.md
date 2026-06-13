# Code Editor Package for Antigravity — Changelog

## [2.0.2] - 2026-06-13

### Added

- **Automatic External Script Editor selection** for Antigravity IDE. After package import and once Unity compile/import settles, the package discovers Antigravity IDE and selects it as Unity's External Script Editor automatically.
- **Recommended C# project generation defaults** now include embedded, local, registry, Git, local tarball and unknown packages, so package asmdefs are included in generated `.csproj` files without manual checkbox setup.

### Changed

- Auto-configuration runs only after Unity is idle and only re-selects Antigravity once per discovered install path, so users can still intentionally switch to another editor later.

## [2.0.1] - 2026-06-10

### Fixed

- **Added missing `.meta` files** for `CITATION.cff`, `CODE_OF_CONDUCT.md`, `CONTRIBUTORS.md` and `SECURITY.md`. UPM packages must ship a `.meta` for every imported file; without them Unity ignores the files and logs "Asset ... has no meta file, but it's in an immutable folder. The asset will be ignored." warnings on every import from the immutable PackageCache. A full repo scan confirmed these four were the only assets missing metas.

### Changed

- `CITATION.cff` `version`: 2.0.0 → 2.0.1.

## [2.0.0] - 2026-05-23

### ⚠️ BREAKING CHANGE — package renamed

The package's UPM `name` field has changed from `com.unity.ide.antigravity` to **`com.badranraza.ide.antigravity`**.

Why: the `com.unity.*` namespace is reserved by Unity Technologies, and OpenUPM rejects packages that use it. Renaming unblocks the OpenUPM listing (https://github.com/openupm/openupm/pull/6490 was closed for exactly this reason) and defends against any future Unity namespace enforcement. No Unity Technologies code is involved in this rename — only the UPM identifier.

**Migrating from v1.x:**

1. In Unity Package Manager, remove the old `com.unity.ide.antigravity` entry.
2. Re-add via the same git URL: `https://github.com/BadranRaza/com.unity.ide.antigravity.git`. Package Manager will install it under the new `com.badranraza.ide.antigravity` name.
3. Your External Tools selection in `Edit > Preferences > External Tools` survives the rename. No code changes are required in your Unity projects.

The GitHub repo URL is unchanged — only the UPM package identifier inside `package.json` changed.

### Changed

- `package.json` `name`: `com.unity.ide.antigravity` → `com.badranraza.ide.antigravity`.
- `CITATION.cff` `version`: 1.0.7 → 2.0.0.
- All Antigravity IDE detection, FAQ, deep-dive doc, issue templates and trust files shipped in v1.0.6–v1.0.8 are carried forward unchanged.

## [1.0.8] - 2026-05-23

### Added

- **Troubleshooting / FAQ section in README** answering the most common questions about the Antigravity 2.0 split: Unity opening the wrong app, the IDE not appearing in the dropdown, IntelliSense, reuse-window not working, and Insider build detection.
- **Comparison table** in README listing the other community Antigravity Unity packages, their last push and Antigravity-2.0-fix status as of May 2026.
- **Deep-dive doc** (`Documentation~/antigravity-2-0-fix.md`) explaining the I/O 2026 product split, why script opens started routing to the agent app, and the three-layer fix in this package (filename filter, manifest verification, narrowed search paths).
- **GitHub issue forms**: bug report, feature request, plus a contact-links config that points open-ended questions at Discussions and Antigravity-IDE bugs at Google's forum.
- **`PULL_REQUEST_TEMPLATE.md`** with a platform-tested checklist.
- **`SECURITY.md`** with private vulnerability reporting instructions and an explicit in/out-of-scope list.
- **`CODE_OF_CONDUCT.md`** adopting Contributor Covenant 3.0 (the version Django moved to in April 2026).
- **`CITATION.cff`** so the repo gets the "Cite this repository" widget.

## [1.0.7] - 2026-05-23

### Changed

- **License reverted to plain MIT.** The PolyForm Noncommercial dual-license briefly introduced in v1.0.6 has been rolled back. The entire package — including v1.0.7 and all earlier versions — is now uniformly governed by the MIT License again. No commercial-license step is required; use freely in personal, commercial and Asset Store projects.
- README polished for discoverability: added release / Unity version / license / stars / forks badges, "Support the project" section (star / share / issues / PRs), and a star-the-repo prompt.

## [1.0.6] - 2026-05-23

### Fixed

- Unity no longer routes C# script opens to the standalone **Antigravity 2.0 agent app**. Google's I/O 2026 release split the product line into the agent-first "Antigravity" desktop app and the VS Code-fork "Antigravity IDE"; previously the package discovered both, so users with both installed could end up launching the agent app (which has no editor surface) on every script open.
  - `IsCandidateForDiscovery` now requires `Antigravity IDE*.exe` / `Antigravity IDE*.app` / `antigravity-ide[-insiders]` filenames.
  - `TryDiscoverInstallation` reads `resources/app/package.json` and rejects any install whose product name is not `Antigravity IDE` (defence-in-depth if a user points External Tools at the agent app manually).
  - `GetVisualStudioInstallations` only lists IDE candidates; the plain `Antigravity.exe` / `antigravity` / `antigravity.desktop` paths were removed on Windows, macOS and Linux.
  - Reuse-existing-window process matching no longer matches the agent app's `Antigravity` / `antigravity` process names.
  - Workspace storage scan now reads `Antigravity IDE/User/workspaceStorage` (the IDE's new per-product AppData / `Application Support` / `.config` location) instead of the legacy `Antigravity/...` path that now belongs to the agent app.
  - External script editor entries are always labelled `Antigravity IDE` — the ambiguous bare `Antigravity` display name was removed.

### Changed

- Insider builds (`Antigravity IDE - Insider.exe`, `antigravity-ide-insiders`, `Antigravity IDE - Insider*.app`) are now recognised on all three platforms.
- Package display name updated to **Antigravity IDE Editor**; package description refreshed to mention the Antigravity 2.0 split explicitly.
- Documentation (`README.md`, `Documentation~/index.md`, `Documentation~/using-visual-studio-editor.md`) updated to reflect the IDE rename, new install paths, and product-name detection.

> **Note:** v1.0.6 originally shipped with a PolyForm Noncommercial dual-license. This was rolled back in v1.0.7 — the entire package is MIT-licensed again. Treat the dual-license note from v1.0.6 release notes as withdrawn.

## [1.0.5] - 2026-03-09

### Fixed

- Removed orphaned `Editor/Plugins.meta` so Unity no longer warns about a missing `Packages/com.unity.ide.antigravity/Editor/Plugins` folder during import

## [1.0.4] - 2026-03-03

### Fixed

- Removed `AppleEventIntegration.bundle` from this package to avoid duplicate editor plugin-name conflicts when `com.boxqkrtm.ide.cursor` is installed

## [1.0.3] - 2026-03-03

### Fixed

- Disabled `AppleEventIntegration.bundle` for this package to avoid duplicate editor plugin-name conflicts when `com.boxqkrtm.ide.cursor` is installed

## [1.0.2] - 2026-03-03

### Fixed

- Regenerated all package `.meta` GUIDs to prevent GUID conflicts with `com.boxqkrtm.ide.cursor` when both packages are installed in the same project

## [1.0.0] - 2026-02-27

### Changed

- Workspace config files (`launch.json`, `settings.json`, `extensions.json`) now written to `.vscode/` — Antigravity reads these as a VS Code fork
- Extensions are looked up from `~/.antigravity/extensions/` (Antigravity's native extension directory)
- Fixed uninitialized `antigravityStoragePath` variable on unsupported platforms (added `#else` early return)
- Removed no-op `IOPath.Combine` wrappers on `GetFolderPath` calls (macOS and Windows)
- Removed orphaned `.meta` files for deleted Cursor and Codium installation classes

## [1.0.0-beta] - 2026-02-25

### Added

- Initial release: Google Antigravity integration for Unity as an external code editor
- Auto-discovery of Antigravity installations on Windows, macOS, and Linux
- Generates `.sln` and `.csproj` files for IntelliSense support
- "Reuse existing Antigravity window" preference toggle
- Workspace detection via Antigravity's `workspaceStorage` directory
