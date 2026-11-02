# Offline FPS

An original, fully offline Android first-person arena game prototype built with Unity. It is an independent project, not a Call of Duty asset or level clone.

## Target
- Android first; tuned initially for mid-range phones such as Redmi 13.
- Offline campaign and offline bot matches.
- Match types: Team Deathmatch and Free For All.
- Local save data; no account or network dependency.
- Stylized, non-graphic sci-fi presentation.

## Unity setup
1. Install Unity Hub on a computer and use a Unity 2022.3 LTS (or newer compatible LTS) editor with Android Build Support, Android SDK & NDK Tools, and OpenJDK.
2. Open this repository as a Unity project.
3. Create a scene named `Assets/Scenes/Main.unity`.
4. Add a GameObject named `GameBootstrap` and attach `Assets/Scripts/Core/GameBootstrap.cs`.
5. Add a player GameObject with a CharacterController, a child Camera, and `FirstPersonController`.
6. Add bot GameObjects with NavMeshAgent and `BotAgent`; bake a NavMesh for the arena.
7. Create UI controls and connect the public methods described in the scripts to Unity UI Buttons / EventTriggers.
8. Add scenes to Build Settings, select Android, then build an APK.

## Current repository status
This is the code foundation and setup guide, not a prebuilt APK or a finished game. Unity Editor is required to create/bake scenes and produce the Android build. The repository intentionally contains no copied commercial game assets.

## Controls
- Desktop editor: WASD + mouse look; Space jump.
- Touch: connect a UI drag area to `TouchLook.SetLookDelta` and on-screen buttons to the movement methods on `FirstPersonController`.
- The prototype uses fictional arena scoring and abstract hit points. It does not model real-world weapons or tactics.
