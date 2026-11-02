using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using OfflineFPS.Core;
using OfflineFPS.Player;
using OfflineFPS.AI;

namespace OfflineFPS.EditorTools
{
    public static class BuildStarterArena
    {
        [MenuItem("Offline FPS/Build Starter Arena")]
        public static void Build()
        {
            if (!AssetDatabase.IsValidFolder("Assets/Editor"))
                AssetDatabase.CreateFolder("Assets", "Editor");
            if (!AssetDatabase.IsValidFolder("Assets/Scenes"))
                AssetDatabase.CreateFolder("Assets", "Scenes");

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            GameObject root = new GameObject("OfflineFPS_Arena");

            GameObject manager = new GameObject("MatchManager");
            manager.transform.SetParent(root.transform);
            manager.AddComponent<MatchManager>();
            manager.AddComponent<MobileQualityController>();

            Material floorMat = MakeMaterial("Arena_Floor", new Color(0.12f, 0.18f, 0.23f));
            Material wallMat = MakeMaterial("Arena_Walls", new Color(0.20f, 0.29f, 0.34f));
            Material checkpointMat = MakeMaterial("Checkpoint_Glow", new Color(0.15f, 0.85f, 0.72f));
            Material botMat = MakeMaterial("Rival_Bot", new Color(0.95f, 0.55f, 0.18f));

            Cube("Arena Floor", new Vector3(0f, -0.5f, 0f), new Vector3(32f, 1f, 32f), floorMat, root.transform);
            Cube("North Wall", new Vector3(0f, 1.5f, 16f), new Vector3(32f, 3f, 1f), wallMat, root.transform);
            Cube("South Wall", new Vector3(0f, 1.5f, -16f), new Vector3(32f, 3f, 1f), wallMat, root.transform);
            Cube("East Wall", new Vector3(16f, 1.5f, 0f), new Vector3(1f, 3f, 32f), wallMat, root.transform);
            Cube("West Wall", new Vector3(-16f, 1.5f, 0f), new Vector3(1f, 3f, 32f), wallMat, root.transform);

            GameObject player = new GameObject("Player");
            player.transform.SetParent(root.transform);
            player.transform.position = new Vector3(0f, 1f, -10f);
            CharacterController cc = player.AddComponent<CharacterController>();
            cc.height = 1.8f; cc.radius = 0.35f; cc.center = new Vector3(0f, 0.9f, 0f);
            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.tag = "MainCamera";
            cameraObject.transform.SetParent(player.transform);
            cameraObject.transform.localPosition = new Vector3(0f, 1.55f, 0f);
            cameraObject.AddComponent<Camera>();
            cameraObject.AddComponent<AudioListener>();
            FirstPersonController controller = player.AddComponent<FirstPersonController>();
            ArenaParticipant participant = player.AddComponent<ArenaParticipant>();
            participant.Configure("Player", TeamId.Blue);
            SerializedObject serialized = new SerializedObject(controller);
            serialized.FindProperty("cameraPivot").objectReferenceValue = cameraObject.transform;
            serialized.ApplyModifiedPropertiesWithoutUndo();

            Vector3[] checkpointPositions = {
                new Vector3(-10f, 1f, -8f), new Vector3(10f, 1f, -8f),
                new Vector3(10f, 1f, 8f), new Vector3(-10f, 1f, 8f)
            };
            Transform[] route = new Transform[checkpointPositions.Length];
            for (int i = 0; i < checkpointPositions.Length; i++)
            {
                GameObject cp = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                cp.name = "Checkpoint_" + (i + 1);
                cp.transform.SetParent(root.transform);
                cp.transform.position = checkpointPositions[i];
                cp.transform.localScale = new Vector3(1.5f, 0.12f, 1.5f);
                cp.GetComponent<Renderer>().sharedMaterial = checkpointMat;
                cp.GetComponent<Collider>().isTrigger = true;
                cp.AddComponent<ArenaCheckpoint>();
                route[i] = cp.transform;
            }

            GameObject bot = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            bot.name = "Arena_Rival_01";
            bot.transform.SetParent(root.transform);
            bot.transform.position = new Vector3(0f, 1f, 10f);
            bot.GetComponent<Renderer>().sharedMaterial = botMat;
            bot.AddComponent<BotAgent>().SetRoute(route);
            ArenaParticipant botParticipant = bot.AddComponent<ArenaParticipant>();
            botParticipant.Configure("Rival 01", TeamId.Orange);

            Light sun = new GameObject("Arena Light").AddComponent<Light>();
            sun.type = LightType.Directional;
            sun.intensity = 1.15f;
            sun.transform.rotation = Quaternion.Euler(48f, -30f, 0f);

            RenderSettings.ambientLight = new Color(0.35f, 0.39f, 0.44f);
            Selection.activeGameObject = player;
            EditorSceneManager.SaveScene(scene, "Assets/Scenes/Main.unity");
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("Offline FPS", "Starter arena created at Assets/Scenes/Main.unity. Press Play to test keyboard movement and checkpoint scoring.", "OK");
        }

        private static void Cube(string name, Vector3 position, Vector3 scale, Material material, Transform parent)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = name; go.transform.SetParent(parent); go.transform.position = position;
            go.transform.localScale = scale; go.GetComponent<Renderer>().sharedMaterial = material;
        }

        private static Material MakeMaterial(string name, Color color)
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null) shader = Shader.Find("Standard");
            Material material = new Material(shader);
            material.name = name; material.color = color;
            return material;
        }
    }
}