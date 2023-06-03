

// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;


// namespace Sandbox.Common.Mods
// {

//   public static class ModManager
//   {

//     private static bool _loaded = false;
//     public static readonly string ModsFolder = Path.GetFullPath(Path.Combine(Application.persistentDataPath, "Mods"));
//     public static Dictionary<string, Mod> Mods = new Dictionary<string, Mod>();
//     public static ModEventBus EventBus = new ModEventBus();

//     public static bool LoadModManager()
//     {
//       if (_loaded) return false;

//       if (Directory.Exists(ModsFolder))
//       {
//         Debug.Log("Mod Folder Exists!");
//       }

//       var files = GetEnabledMods();
//       foreach (var file in files)
//       {
//         try
//         {
//           Debug.Log(file);
//           LoadMod(file);
//         }
//         catch (Exception e)
//         {
//           Debug.LogError(e);
//         }
//       }
//       _loaded = true;
//       return true;
//     }

//     public static string[] GetEnabledMods()
//     {
//       string[] re = File.ReadAllLines(Path.Combine(ModsFolder, "enabled.txt"));
//       return re;
//     }

//     public static string[] GetMods()
//     {
//       string[] re = Directory.GetDirectories(ModsFolder);
//       // filter out the actual directories
//       for (int i = 0; i < re.Length; i++)
//       {
//         re[i] = Path.GetFileName(re[i]);
//       }
//       re = Array.FindAll(re, s => !s.StartsWith("."));

//       return re;
//     }

//     public static bool LoadMod(string modID)
//     {
//       Debug.Log("Loading Mod: " + modID);
//       if (Mods.ContainsKey(modID)) return false;
//       // TODO LATER : Load from addressable
//       // right now we load from scene
//       Mods.Add(modID, GameObject.Find(modID + "Mod").GetComponent<Mod>());

//       // AssemblyReferenceAsset assemblyReferenceAsset = UnityEngine.Resources.Load<AssemblyReferenceAsset>("Assemblies/Microsoft.CSharp");
//       // Debug.Log(assemblyReferenceAsset);
//       // Debug.Log(assemblyReferenceAsset.IsValid);
//       // domain.RoslynCompilerService.ReferenceAssemblies.Add(assemblyReferenceAsset);
//       // // domain.RoslynCompilerService.ReferenceAssemblies.Add(typeof(Sandbox.Server.Mods.Mod).Assembly);
//       // // get all files in folder
//       // // var files = Directory.GetFiles(Path.Combine(ModsFolder, modID.ToLower()), "*.cs", SearchOption.AllDirectories);
//       // // foreach (var file in files)
//       // // {
//       // //   domain.CompileAndLoadDirectory(file);
//       // // }
//       // // domain.CompileAndLoadDirectory(Path.Combine(ModsFolder, modID.ToLower()));
//       // // var file = Path.Combine(ModsFolder, modID.ToLower(), modID.ToLower() + ".csproj");

//       // // if (!File.Exists(file))
//       // // {
//       // //   Debug.LogError("Failed to load mod: " + modID + " because " + file + " does not exist!");
//       // //   return false;
//       // // }

//       // string[] files = Directory.GetFiles(Path.Combine(ModsFolder, modID.ToLower()), "*.cs", SearchOption.AllDirectories).Where(s => !s.EndsWith(".g.cs")).ToArray();
//       // ScriptAssembly asm = domain.CompileAndLoadFiles(files);


//       // // Debug.Log(file);
//       // // ScriptAssembly asm = domain.CompileAndLoadCSharpProject(file);
//       // // if (asm == null)
//       // // {
//       // //   Debug.LogError("Failed to load mod: " + modID);
//       // //   return false;
//       // // }
//       // // Debug.Log(asm);
//       // // ScriptAssembly asm = domain.CompileAndLoadFile(Path.Combine(ModsFolder, modID.ToLower(), modID + "Mod.cs"));
//       // Mods.Add(modID, asm);
//       return true;
//     }
//   }
// }