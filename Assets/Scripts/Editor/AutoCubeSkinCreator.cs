using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Assets.Scripts;
using System.IO;

public class AutoCubeSkinCreator : EditorWindow
{
    private string assetName = "NewSkin";
    private static string prefabPath = "Assets/Prefabs/BaseCubeSkin.prefab"; // Örnek prefab yolu
    private static string skinsFolderPath = "Assets/Skins"; // Skins ana klasörü

    [MenuItem("Tools/Skin Creator")]
    public static void ShowWindow()
    {
        GetWindow<AutoCubeSkinCreator>("Skin Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Yeni Skin Oluştur", EditorStyles.boldLabel);
        assetName = EditorGUILayout.TextField("İsim:", assetName);

        if (GUILayout.Button("Oluştur"))
        {
            CreateAssets();
        }
    }

    private void CreateAssets()
    {
        if (string.IsNullOrEmpty(assetName))
        {
            EditorUtility.DisplayDialog("Hata", "Geçerli bir isim girin!", "Tamam");
            return;
        }

        // "Assets/Skins" klasörünü oluştur
        if (!AssetDatabase.IsValidFolder(skinsFolderPath))
        {
            AssetDatabase.CreateFolder("Assets", "Skins");
        }

        // Yeni skin klasörünü oluştur
        string folderPath = $"{skinsFolderPath}/{assetName}";
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            AssetDatabase.CreateFolder(skinsFolderPath, assetName);
        }

        // Prefab Kopyalama
        string newPrefabPath = $"{folderPath}/{assetName}.prefab";
        if (File.Exists(prefabPath))
        {
            AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(newPrefabPath);
            prefab.name = assetName;

            // Material Oluşturma
            Material newMaterial = new Material(Shader.Find("Standard"));
            AssetDatabase.CreateAsset(newMaterial, $"{folderPath}/{assetName}.mat");

            // Material Ayarları
            Cube cube = prefab.GetComponent<Cube>();
            cube.modelTransform.GetComponent<Renderer>().material = newMaterial;

            // ScriptableObject Oluşturma
            SkinDataSO newSO = CreateInstance<SkinDataSO>();
            newSO.name = assetName;
            newSO.prefab = prefab;
            AssetDatabase.CreateAsset(newSO, $"{folderPath}/{assetName}.asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.DisplayDialog("Başarılı", $"'{assetName}' için varlıklar oluşturuldu.", "Tamam");
        }
        else
        {
            Debug.LogError("Belirtilen prefab bulunamadı!");
        }


    }
}
