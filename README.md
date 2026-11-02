# Offline FPS — Offline Arena Prototype

An original, offline first-person arena-racing prototype for Android, built with Unity. It uses a clean sci-fi sports-arena theme: collect checkpoints, race a waypoint rival, and earn points. There is no real-world weapon simulation, graphic violence, account, or network dependency.

## Current prototype systems
- First-person movement and camera look.
- Touch movement pad and right-side drag-to-look controls generated in the starter scene.
- Free For All scoring and team checkpoint scoring.
- Checkpoint trigger zones with a per-participant cooldown.
- Lightweight offline rival bot that follows a waypoint loop.
- Local quality presets and a 60 FPS target for mid-range Android devices.
- Unity Editor menu command that generates a starter arena scene and saves its materials.

## Generate the starter scene
1. Open this repository in Unity **2022.3.62f1** or a compatible Unity 2022.3 LTS editor.
2. Wait for package import and script compilation to finish.
3. In the Unity menu, select **Offline FPS → Build Starter Arena**.
4. The tool creates `Assets/Scenes/Main.unity`, arena materials, a player, touch controls, four checkpoints, a rival, lighting, and the match manager.
5. Press Play to test. In the Editor, use WASD and mouse; on Android, use the on-screen left movement pad and drag the right side to look.

## Android build
Install Unity Android Build Support (including Android SDK & NDK Tools and OpenJDK) through Unity Hub. Open Build Settings, select Android, add `Assets/Scenes/Main.unity` to Scenes In Build, switch platform, and build an APK.

## Important status
This repository now contains the gameplay scripts and a one-click scene generator. The scene generator must be run inside the Unity Editor; a finished APK has **not** been built or tested yet. Unity Editor and Android Build Support are required for the actual Android build. This repository does not contain copied commercial game assets.

## Device tuning
The default settings favor responsiveness and modest rendering cost. The quality controller exposes Performance, Balanced, and Quality presets. Test on the target phone before release and adjust resolution, lighting, and frame-rate settings based on actual device performance.
